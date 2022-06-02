using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services.User;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;

namespace Api.Application.Test.User.WhenRequestCreate
{
    public class Return_Created
    {
        private UsersController? _controller;

        [Fact(DisplayName = "Is Possible Do Created")]
        public async Task Is_Possible_Do_Created()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(x => x.Post(It.IsAny<UserDtoCreate>())).ReturnsAsync(
                new UserDtoCreateResult
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var userDtoCreate = new UserDtoCreate
            {
                Name = name,
                Email = email
            };

            var result = await _controller.Post(userDtoCreate);
            Assert.True(result is CreatedResult);

            var resultValue = ((CreatedResult)result).Value as UserDtoCreateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(userDtoCreate.Name, resultValue?.Name);
            Assert.Equal(userDtoCreate.Email, resultValue?.Email);
        }
    }
}