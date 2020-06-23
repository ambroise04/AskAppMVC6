using AskAppMVC6.Common.Enumerations;
using AskAppMVC6.DAL.Context;
using AskAppMVC6.DAL.Entities;
using AskAppMVC6.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace AskAppMVC6.DAL.Tests.Questions
{
    [TestClass]
    public class GetByPredicateQuestionsTests
    {
        [TestMethod]
        public void GetByPredicateQuestions_AddThreeNewQuestionsThenRetrieveOneQuestion_ReturnCorrectQuestion()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new QuestionRepository(context);
            var question1 = new Question 
            { 
                Message = "Test question 1", 
                PostDate = DateTime.Now, 
                State = QuestionState.Waiting, 
                IsDeleted = false, 
               Requester = new ApplicationUser { Id = "aaaaabbbbbcccccdddd", Email="test1@test.be"}
            };
            var question2 = new Question
            {
                Message = "Test question 2",
                PostDate = DateTime.Now,
                State = QuestionState.Waiting,
                IsDeleted = false,
                Requester = new ApplicationUser { Id = "bbbbbcccccddddeeeeee", Email = "test1@test.be" }
            };
            var question3 = new Question
            {
                Message = "Test question 3",
                PostDate = DateTime.Now,
                State = QuestionState.Waiting,
                IsDeleted = false,
                Requester = new ApplicationUser { Id = "cccccbbbbbcccccdddd", Email = "test1@test.be" }
            };
            var addedQuestion1 = repository.Insert(question1);
            var addedQuestion2 = repository.Insert(question2);
            var addedQuestion3 = repository.Insert(question3);
            context.SaveChanges();
            //ACT
            var result = repository.GetByPredicate(q => q.Message.Equals("Test question 2"));
            //ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Test question 2", result.First().Message);
        }
    }
}