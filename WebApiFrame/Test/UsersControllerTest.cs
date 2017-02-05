using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrame.Controllers;
using WebApiFrame.Models;
using WebApiFrame.Repository;
using Xunit;

namespace WebApiFrame.Test
{
    public class UsersControllerTest
    {
        private readonly UsersController _controller;

        public UsersControllerTest(ILogger<UsersController> logger)
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetUsers());
            _controller = new UsersController(logger, mockRepo.Object);
        }

        //[Fact]
        public void GetAllTest()
        {
            IActionResult actionResult = _controller.GetAll();
            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            var result = Assert.IsAssignableFrom<IEnumerable<User>>(objectResult.Value);
            Assert.Equal(3, result.Count());
        }

        private IEnumerable<User> GetUsers()
        {
            return new List<User>()
            {
                new User(){ Id = 1, Name = "name:1", Sex = "Male" },
                new User(){ Id = 2, Name = "name:2", Sex = "Female" },
                new User(){ Id = 3, Name = "name:3", Sex = "Male" },
            };
        }
    }
}
