using FluentAssertions;
using GitUsers.API.Config;
using GitUsers.API.Models;
using GitUsers.API.Services;
using GitUsers.UnitTests.Fixtures;
using GitUsers.UnitTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace GitUsers.UnitTests.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_UsingHTTPGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.
                SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new GitApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);


            //Act
            await sut.RetrieveUsers(new List<string>());

            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );

        }

        [Fact]
        public async Task GetAllUsers_WhenHits440_ReturnsEmptyListofUsers()
        {
            //Arrange

            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new GitApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);


            //Act 
            var result = await sut.RetrieveUsers(new List<string>());

            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.
                SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com";
            var config = Options.Create(new GitApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);


            //Act
            var result = await sut.RetrieveUsers(new List<string>());

            //Assert
            result.Count.Should().Be(4);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var endpoint = "https://example.com/users";
             var handlerMock = MockHttpMessageHandler<User>.
                SetupBasicGetResourceList(expectedResponse, endpoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new GitApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);


            //Act
            var result = await sut.RetrieveUsers(new List<string>());
            var uri = new Uri(endpoint);

            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get 
                && req.RequestUri== uri),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}
