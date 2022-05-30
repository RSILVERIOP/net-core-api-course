using Moq;
using Xunit;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Dtos.User;

namespace Api.Services.Test.User
{
    public class WhenCreateIsExecuted : UserTest
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "Is possible execute Create")]
        public async Task Is_Possible_Execute_Create()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Post(userDtoCreate)).ReturnsAsync(userDtoCreateResult);
            _service = _serviceMock.Object;

            var result  = await _service.Post(userDtoCreate);

            Assert.NotNull(result);
            Assert.Equal(NameUser, result.Name);
            Assert.Equal(EmailUser, result.Email);
        }
    }
}