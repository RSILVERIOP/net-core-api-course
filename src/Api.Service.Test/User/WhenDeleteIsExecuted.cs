using Moq;
using Xunit;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Dtos.User;

namespace Api.Services.Test.User
{
    public class WhenDeleteIsExecuted : UserTest
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "Is possible execute Delete")]
        public async Task Is_Possible_Execute_Delete()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result  = await _service.Delete(IdUser);
            Assert.True(result);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;

            result  = await _service.Delete(Guid.NewGuid());
            Assert.False(result);
        }
    }
}