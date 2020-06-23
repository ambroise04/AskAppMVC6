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
    public class UpdateQuestionTests
    {
        [TestMethod]
        public void UpdateQuestion_NullQuestionSubmitted_ThrowArgumentNullException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new QuestionRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => repository.Update(null));
        }

        [TestMethod]
        public void UpdateQuestion_QuestionWithBadIdSubmitted_ThrowArgumentException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new QuestionRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentException>(() => repository.Update(new Question {Id = 0, Message="Test question 1" }));
            Assert.ThrowsException<ArgumentException>(() => repository.Update(new Question {Id = -1, Message="Test question 2" }));
        }

        [TestMethod]
        public void UpdateQuestion_CorrectQuestionSubmitted_ReturnUpdatedQuestion()
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
            addedQuestion.State = QuestionState.Answered;
            var result = repository.Update(addedQuestion);
            repository.Save();
            //ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual(QuestionState.Answered, result.State);
        }
    }
}