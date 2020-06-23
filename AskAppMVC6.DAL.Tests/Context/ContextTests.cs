using AskAppMVC6.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AskAppMVC6.DAL.Tests.Context
{
    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void ParameterlessCtor_CheckWheterSqliteIsUsedAndDBCanBeConnected()
        {
            //ARRANGE
            var context = new ApplicationContext();
            context.Database.EnsureCreated();
            //ACT
            //ASSERT
            Assert.IsTrue(context.Database.IsSqlite());
            Assert.IsTrue(context.Database.CanConnect());
        }

        [TestMethod]
        public void Ctor_InitializedWithNullOption_()
        {
            //ARRANGE
            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => new ApplicationContext(null));
        }
    }
}