using AskAppMVC6.Common.Enumerations;
using AskAppMVC6.DAL.Context;
using AskAppMVC6.DAL.Entities;
using AskAppMVC6.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace AskAppMVC6.DAL.Tests.Responses
{
    [TestClass]
    public class GetResponseTests
    {
        [TestMethod]
        public void GetResponse_BadResponseIdSubmitted_ThrowArgumentException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new ResponseRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentException>(() => repository.Get(0));
            Assert.ThrowsException<ArgumentException>(() => repository.Get(-2));
        }

        [TestMethod]
        public void GetResponse_AddNewResponseThenRetrieveAddedResponse_ReturnAddedResponseNotNull()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new ResponseRepository(context);
            var repositoryQst = new QuestionRepository(context);
            //Question
            var question = new Question
            {
                Message = "Test question",
                PostDate = DateTime.Now,
                State = QuestionState.Waiting,
                IsDeleted = false
            };
            var addedQuestion = repositoryQst.Insert(question);
            context.SaveChanges();
            //Responses
            var response = new Response
            {
                Question = addedQuestion,
                DateOfResponse = DateTime.Now,
                IsDeleted = false,
                Responder = new ApplicationUser { Id = "aaaaabbbbbcccccdddd", Email = "test@test.be" }
            };
            var addedResponse = repository.Insert(response);
            context.SaveChanges();
            //ACT
            var result = repository.Get(addedResponse.Id);
            //ASSERT
            Assert.IsNotNull(addedResponse);
            Assert.AreNotEqual(0, addedResponse.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual("aaaaabbbbbcccccdddd", result.Responder.Id);
        }
    }
}