using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services.User;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;

namespace Api.Application.Test.User.WhenRequestUpdate
{
    public class Return_Update
    {
        private UsersController? _controller;

        [Fact(DisplayName = "Is Possible Do Update")]
        public async Task Is_Possible_Do_Update()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(x => x.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
                new UserDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    UpdateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);

            var userDtoUpdate = new UserDtoUpdate
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email
            };

            var result = await _controller.Put(userDtoUpdate);
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UserDtoUpdateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(userDtoUpdate.Name, resultValue?.Name);
            Assert.Equal(userDtoUpdate.Email, resultValue?.Email);
        }
    }
}