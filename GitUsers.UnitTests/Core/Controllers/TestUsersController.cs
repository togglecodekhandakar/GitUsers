using FluentAssertions;
using GitUsers.API.Controllers;
using GitUsers.API.Models;
using GitUsers.API.Services.Interface;
using GitUsers.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GitUsers.UnitTests.Core.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            // Arrange
            var moqUsersService = new Mock<IUsersService>();
            moqUsersService
                .Setup(service => service.RetrieveUsers(new List<string>()))
                .ReturnsAsync(UsersFixtures.GetTestUsers());
                

            var sut = new RetriveUsers(moqUsersService.Object);


            // Act

            var result = (OkObjectResult)await sut.Get(new List<string>());


            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
        {
            // Arrange
            var moqUsersService = new Mock<IUsersService>();
            moqUsersService
                .Setup(service => service.RetrieveUsers(new List<string>()))
                .ReturnsAsync(new List<User>());


            var sut = new RetriveUsers(moqUsersService.Object);


            // Act

            var result = await sut.Get(new List<string>());


            // Assert
            moqUsersService.Verify(
                service =>  service.RetrieveUsers(new List<string>()), 
                Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {

            //Arrange
            var moqUsersService = new Mock<IUsersService>();
            moqUsersService
                .Setup(service => service.RetrieveUsers(new List<string>()))
                .ReturnsAsync(UsersFixtures.GetTestUsers());

            var sut = new RetriveUsers(moqUsersService.Object);

            //Act 

            var result = await sut.Get(new List<string>());

            //Assert

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();

        }

        [Fact]
        public async Task Get_OnNoUserFound_Returns404()
        {

            //Arrange
            var moqUsersService = new Mock<IUsersService>();
            moqUsersService
                .Setup(service => service.RetrieveUsers(new List<string>()))
                .ReturnsAsync(new List<User>());

            var sut = new RetriveUsers(moqUsersService.Object);

            //Act 

            var result = await sut.Get(new List<string>());

            //Assert

            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
            

        }



    }
}