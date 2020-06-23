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
    public class AddQuestionTests
    {
        [TestMethod]
        public void AddQuestion_NullQuestionSubmitted_ThrowArgumentNullException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new QuestionRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => repository.Insert(null));
        }

        [TestMethod]
        public void AddQuestion_CorrectQuestionSubmitted_ReturnAddedQuestionNotNull()
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
            //ACT
            var result = repository.Insert(question);
            repository.Save();
            //ASSERT
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Id);
            Assert.AreEqual("Test question", result.Message);
        }
    }
}