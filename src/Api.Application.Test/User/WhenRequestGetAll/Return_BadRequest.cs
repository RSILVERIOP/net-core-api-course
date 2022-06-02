using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services.User;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;

namespace Api.Application.Test.User.WhenRequestGetAll
{
    public class Return_BadRequest
    {
        private UsersController? _controller;

        [Fact(DisplayName = "Is Possible Do Bad Request")]
        public async Task Is_Possible_Do_BadRequest()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(
                new List<UserDto>
                {
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow
                    },
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow
                    }
                }
            );

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Invalid Format");

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}