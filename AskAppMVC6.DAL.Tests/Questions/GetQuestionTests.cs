using AskAppMVC6.Common.Enumerations;
using AskAppMVC6.DAL.Context;
using AskAppMVC6.DAL.Entities;
using AskAppMVC6.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace AskAppMVC6.DAL.Tests.Questions
{
    [TestClass]
    public class GetQuestionTests
    {
        [TestMethod]
        public void GetQuestion_BadQuestionIdSubmitted_ThrowArgumentException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new QuestionRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentException>(() => repository.Get(0));
            Assert.ThrowsException<ArgumentException>(() => repository.Get(-2));
        }

        [TestMethod]
        public void GetQuestion_AddNewQuestionThenRetrieveAddedQuestion_ReturnAddedQuestionNotNull()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new QuestionRepository(context);
            var question = new Question 
            { 
                Message = "Test question", 
                PostDate = DateTime.Now, 
                State = QuestionState.Waiting, 
                IsDeleted = false, 
               Requester = new ApplicationUser { Id = "aaaaabbbbbcccccdddd", Email="test@test.be"}
            };
            var addedQuestion = repository.Insert(question);
            context.SaveChanges();
            //ACT
            var result = repository.Get(addedQuestion.Id);
            //ASSERT
            Assert.IsNotNull(addedQuestion);
            Assert.AreNotEqual(0, addedQuestion.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test question", result.Message);
        }
    }
}