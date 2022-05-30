using Moq;
using Xunit;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Dtos.User;

namespace Api.Services.Test.User
{
    public class WhenUpdateIsExecuted : UserTest
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "Is possible execute Update")]
        public async Task Is_Possible_Execute_Update()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Post(userDtoCreate)).ReturnsAsync(userDtoCreateResult);
            _service = _serviceMock.Object;

            var result  = await _service.Post(userDtoCreate);
            Assert.NotNull(result);
            Assert.Equal(NameUser, result.Name);
            Assert.Equal(EmailUser, result.Email);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Put(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);
            _service = _serviceMock.Object;

            var resultUpdate  = await _service.Put(userDtoUpdate);
            Assert.NotNull(resultUpdate);
            Assert.Equal(NameUserUpdated, resultUpdate.Name);
            Assert.Equal(EmailUserUpdated, resultUpdate.Email);

        }
    }
}