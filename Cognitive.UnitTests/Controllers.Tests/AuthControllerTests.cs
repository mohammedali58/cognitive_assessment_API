using APP.Controllers;
using Application.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Cognitive.UnitTests.Tests.Controllers.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _configMock = new Mock<IConfiguration>();

            // Provide dummy JWT config
            _configMock.Setup(c => c["JWT_Key"]).Returns("ThisIsASuperSecureJWTKeyOf32+Bytes!");
            _configMock.Setup(c => c["JWT_Issuer"]).Returns("test-issuer");
            _configMock.Setup(c => c["JWT_Audience"]).Returns("test-audience");

            _controller = new AuthController(_userRepoMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_IfUserAlreadyExists()
        {
            // Arrange
            var dto = new UserDto { Username = "existing", Password = "pass" };
            _userRepoMock.Setup(r => r.GetByUsernameAsync("existing"))
                         .ReturnsAsync(new User(){ Username = "existing", PasswordHash = "dummy" });

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User already exists.", badRequest.Value);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsCreated()
        {
            // Arrange
            var dto = new UserDto { Username = "newuser", Password = "pass" };
            _userRepoMock.Setup(r => r.GetByUsernameAsync("newuser"))
                         .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = okResult.Value;
            var message = value?.GetType().GetProperty("message")?.GetValue(value) as string;

            Assert.Equal("User created", message);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_IfUserDoesNotExist()
        {
            // Arrange
            var dto = new UserDto { Username = "notfound", Password = "pass" };
            _userRepoMock.Setup(r => r.GetByUsernameAsync("notfound"))
                         .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_IfPasswordIsIncorrect()
        {
            // Arrange
            var dto = new UserDto { Username = "test", Password = "wrong" };
            var user = new User { Id = 1, Username = "test", PasswordHash = BCrypt.Net.BCrypt.HashPassword("correct") };

            _userRepoMock.Setup(r => r.GetByUsernameAsync("test")).ReturnsAsync(user);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnAccessToken_WhenCredentialsAreValid()
        {
            // Arrange
            var dto = new UserDto { Username = "valid", Password = "password123" };
            var user = new User { Id = 42, Username = "valid", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123") };

            _userRepoMock.Setup(r => r.GetByUsernameAsync("valid")).ReturnsAsync(user);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var value = ok.Value;

            var token = (string?)value?.GetType().GetProperty("access_token")?.GetValue(value);
            Assert.False(string.IsNullOrWhiteSpace(token));
        }
    }
}
