using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services.User;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;

namespace Api.Application.Test.User.WhenRequestDelete
{
    public class Return_BadRequest
    {
        private UsersController? _controller;

        [Fact(DisplayName = "Is Possible Do Bad Request")]
        public async Task Is_Possible_Do_BadRequest()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(false);

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Invalid format");

            var result = await _controller.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }
    }
}