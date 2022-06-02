using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services.User;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;

namespace Api.Application.Test.User.WhenRequestGetAll
{
    public class Return_GetAll
    {
        private UsersController? _controller;

        [Fact(DisplayName = "Is Possible Do GetAll")]
        public async Task Is_Possible_Do_Get()
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

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue =  ((OkObjectResult)result).Value as IEnumerable<UserDto>;
            Assert.True(resultValue?.Count() == 2);
        }
    }
}