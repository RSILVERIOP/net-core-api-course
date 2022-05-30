using Moq;
using Xunit;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Dtos.User;

namespace Api.Services.Test.User
{
    public class WhenGetIsExecuted : UserTest
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "Is possible execute GET")]
        public async Task Is_Possible_Execute_Get()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Get(IdUser)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;

            var result  = await _service.Get(IdUser);

            Assert.NotNull(result);
            Assert.True(result.Id == IdUser);
            Assert.Equal(NameUser, result.Name);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto) null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(IdUser);
            Assert.Null(_record);
        }
    }
}