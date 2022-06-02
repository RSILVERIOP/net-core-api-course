using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services.User;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;

namespace Api.Application.Test.User.WhenRequestDelete
{
    public class Return_Deleted
    {
        private UsersController? _controller;

        [Fact(DisplayName = "Is Possible Do Delete")]
        public async Task Is_Possible_Do_Deleted()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue =  ((OkObjectResult)result).Value;
            Assert.NotNull(resultValue);
            Assert.True(resultValue == null ? false : (Boolean)resultValue);
        }
    }
}