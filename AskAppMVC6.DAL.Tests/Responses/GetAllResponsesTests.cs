using AskAppMVC6.Common.Enumerations;
using AskAppMVC6.DAL.Context;
using AskAppMVC6.DAL.Entities;
using AskAppMVC6.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace AskAppMVC6.DAL.Tests.Responses
{
    [TestClass]
    public class GetAllResponsesTests
    {
        [TestMethod]
        public void GetAllResponses_AddThreeNewResponsesThenRetrieveAddedResponses_ReturnCorrectNumberOfResponses()
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
            var response1 = new Response 
            { 
                Question = addedQuestion,
                DateOfResponse = DateTime.Now, 
                IsDeleted = false, 
                Responder = new ApplicationUser { Id = "aaaaabbbbbcccccdddd", Email="test1@test.be"}
            };
            var response2 = new Response
            {
                Question = addedQuestion,
                DateOfResponse = DateTime.Now,
                IsDeleted = false,
                Responder = new ApplicationUser { Id = "bbbbbeeebcccccdddd", Email = "test1@test.be" }
            };
            var response3 = new Response
            {
                Question = addedQuestion,
                DateOfResponse = DateTime.Now,
                IsDeleted = false,
                Responder = new ApplicationUser { Id = "ccccbbbbbcccccdddd", Email = "test1@test.be" }
            };
            var addedResponse1 = repository.Insert(response1);
            var addedResponse2 = repository.Insert(response2);
            var addedResponse3 = repository.Insert(response3);
            context.SaveChanges();
            //ACT
            var result = repository.GetAll();
            //ASSERT
            Assert.IsNotNull(addedResponse1);
            Assert.IsNotNull(addedResponse2);
            Assert.IsNotNull(addedResponse3);
            Assert.AreEqual(3, result.Count());
        }
    }
}