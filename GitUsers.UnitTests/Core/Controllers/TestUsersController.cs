using FluentAssertions;
using GitUsers.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace GitUsers.UnitTests.Core.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            // Arrange

            var sut = new UsersController();


            // Act

            var result = (OkObjectResult)await sut.Get();


            // Assert
            result.StatusCode.Should().Be(200);
        }

        
    }
}