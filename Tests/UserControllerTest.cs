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
            UserController.free_id = 3;
            controller = new UserController();
        }

        // ... other tests ...

        [TestMethod]
        public void Create()
        {
            // Act
            User newUser = new User { Name = "User3" };
            RedirectToRouteResult result = controller.Create(newUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.RouteValues["action"]);
            Assert.AreEqual(3, result.RouteValues["id"]);
            Assert.AreEqual(3, UserController.userlist.Count);
            Assert.AreEqual(newUser.Name, UserController.userlist.Last().Name);
        }

        [TestMethod]
        public void Edit_UserExists()
        {
            // Act
            User updatedUser = new User { Name = "UpdatedUser1" };
            RedirectToRouteResult result = controller.Edit(1, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["id"]);
            Assert.AreEqual(updatedUser.Name, UserController.userlist.First(u => u.Id == 1).Name);
        }

        [TestMethod]
        public void Edit_UserDoesNotExist()
        {
            // Act
            User updatedUser = new User { Name = "UpdatedUser1" };
            HttpNotFoundResult result = controller.Edit(999, updatedUser) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_UserExists()
        {
            // Act
            RedirectToRouteResult result = controller.Delete(1, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(UserController.userlist.Any(u => u.Id == 1));
        }

        [TestMethod]
        public void Delete_UserDoesNotExist()
        {
            // Act
            HttpNotFoundResult result = controller.Delete(999, null) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
