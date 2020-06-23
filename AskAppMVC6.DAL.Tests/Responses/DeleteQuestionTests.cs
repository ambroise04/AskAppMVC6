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
    public class DeleteResponseTests
    {
        [TestMethod]
        public void DeleteResponse_NullResponseSubmitted_ThrowArgumentNullException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new ResponseRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => repository.Delete(null));
        }

        [TestMethod]
        public void DeleteResponse_ResponseWithBadIdSubmitted_ThrowArgumentException()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name).Options;
            var context = new ApplicationContext(options);
            var repository = new ResponseRepository(context);
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentException>(() => repository.Delete(new Response { Id = 0 }));
            Assert.ThrowsException<ArgumentException>(() => repository.Delete(new Response { Id = -1 }));
        }

        [TestMethod]
        public void DeleteResponse_CorrectResponseSubmitted_ThrowArgumentException()
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
            var addedResponse = repository.Insert(response);
            context.SaveChanges();
            //ACT            
            var result = repository.Delete(addedResponse);
            repository.Save();
            //ASSERT
            Assert.IsTrue(result);
        }
    }
}