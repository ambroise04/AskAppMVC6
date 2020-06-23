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
    public class AddResponseTests
    {
        [TestMethod]
        public void AddResponse_NullResponseSubmitted_ThrowArgumentNullException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new ResponseRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => repository.Insert(null));
        }

        [TestMethod]
        public void AddResponse_CorrectResponseSubmitted_ReturnAddedResponseNotNull()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new ResponseRepository(context);
            var response = new Response
            {
                Question = new Question { Id = 1, Message = "Test question 1" },
                DateOfResponse = DateTime.Now,
                IsDeleted = false,
                Responder = new ApplicationUser { Id = "aaaaabbbbbcccccdddd", Email = "test@test.be" }
            };
            //ACT
            var result = repository.Insert(response);
            repository.Save();
            //ASSERT
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Id);
        }
    }
}