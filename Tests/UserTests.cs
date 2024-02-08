using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace Tests
{
    public class UserTests
    {
        private Mock<IUserAppService> _userAppServiceMock;
        private UserManagmentController _controller;

        public UserTests()
        {
            _userAppServiceMock = new Mock<IUserAppService>();
            _controller = new UserManagmentController(_userAppServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var userDto = new UserDto
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "P@ssw0rd"
            };
            IdentityResult expectedResult = IdentityResult.Success;
            _userAppServiceMock.Setup(x => x.CreateUserAsync(userDto.Username, userDto.Email, userDto.Password)).Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var userDto = new UserDto
            {
                Username = null,
                Email = null,
                Password = null
            };

            IdentityResult expectedResult = IdentityResult.Success;
            _userAppServiceMock.Setup(x => x.CreateUserAsync(userDto.Username, userDto.Email, userDto.Password))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetUserById_ExistinguserName_ReturnsOkResult()
        {
            // Arrange
            var userDto = new UserDto();
            var userId = new Guid("48af4518-d40c-43bb-9dc2-ebb25725765d").ToString();
            _userAppServiceMock.Setup(x => x.GetUserByIdAsync(userId))
                .ReturnsAsync(userDto);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetUserById_NonExistinguserName_ReturnsNotFoundResult()
        {
            // Arrange
            var userName = "arad";
            var userDto = new UserDto();
            _userAppServiceMock.Setup(x => x.GetUserByIdAsync(userName)).ReturnsAsync(userDto);

            // Act
            var result = await _controller.GetUserById(userName);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUserByUsername_ExistingUsername_ReturnsOkResult()
        {
            // Arrange
            var username = "testuser";
            var userDto = new UserDto { Username = username };
            _userAppServiceMock.Setup(x => x.GetUserByUsernameAsync(username))
                .ReturnsAsync(userDto);

            // Act
            var result = await _controller.GetUserByUsername(username);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUserByUsername_NonExistingUsername_ReturnsNotFoundResult()
        {
            // Arrange
            var userDto = new UserDto();
            var username = "testuser";
            _userAppServiceMock.Setup(x => x.GetUserByUsernameAsync(username))
                .ReturnsAsync(userDto);

            // Act
            var result = await _controller.GetUserByUsername(username);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    public class ServiceResult
    {
        public string Errors { get; set; }
    }
}