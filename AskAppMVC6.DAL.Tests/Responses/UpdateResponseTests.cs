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
    public class UpdateResponseTests
    {
        [TestMethod]
        public void UpdateResponse_NullResponseSubmitted_ThrowArgumentNullException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new ResponseRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => repository.Update(null));
        }

        [TestMethod]
        public void UpdateResponse_ResponseWithBadIdSubmitted_ThrowArgumentException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new ResponseRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentException>(() => repository.Update(new Response {Id = 0}));
            Assert.ThrowsException<ArgumentException>(() => repository.Update(new Response {Id = -1}));
        }

        [TestMethod]
        public void UpdateResponse_CorrectResponseSubmitted_ReturnUpdatedResponse()
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
            addedResponse.IsDeleted = true;
            var result = repository.Update(addedResponse);
            repository.Save();
            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsDeleted);
        }
    }
}