using Moq;
using Xunit;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Dtos.User;

namespace Api.Services.Test.User
{
    public class WhenGetAllIsExecuted : UserTest
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "Is possible execute GETAll")]
        public async Task Is_Possible_Execute_GetAll()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listUserDto);
            _service = _serviceMock.Object;

            var result  = await _service.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);

            var _listResult = new List<UserDto>();
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(_listResult.AsEnumerable);
            _service = _serviceMock.Object;

            var _resultEmpty = await _service.GetAll();
            Assert.Empty(_resultEmpty);
            Assert.True(_resultEmpty.Count() == 0);
        }
    }
}