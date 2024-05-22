using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController controller;
        private List<User> users;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            users = new List<User>
            {
                new User { Id = 1, Name = "User1" },
                new User { Id = 2, Name = "User2" }
            };

            UserController.userlist = users;
            controller = new UserController();
        }

        [TestMethod]
        public void Index()
        {
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            CollectionAssert.AreEqual(users, (List<User>)result.ViewData.Model);
        }

        [TestMethod]
        public void Details_UserExists()
        {
            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.AreEqual(users.First(), result.ViewData.Model);
        }

        [TestMethod]
        public void Details_UserDoesNotExist()
        {
            // Act
            HttpNotFoundResult result = controller.Details(999) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
