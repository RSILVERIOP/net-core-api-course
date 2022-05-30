using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class WhenFindByLoginIsExecuted
    {
        private ILoginService? _service;
        private Mock<ILoginService>? _serviceMock;

        [Fact(DisplayName = "Is Possible Execute Method FindByLogin")]
        public async Task Is_Possible_Execute_Method_FindByLogin()
        {
            var email = Faker.Internet.Email();
            var returnObject = new {
                authenticated = true,
                created = DateTime.UtcNow,
                expiration = DateTime.UtcNow.AddHours(8),
                acessToken = Guid.NewGuid(),
                userName = email,
                name = Faker.Name.FullName(),
                message = "User logged in successfully"
            };

            var loginDto = new LoginDto
            {
                Email = email
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(x => x.FindByLogin(loginDto)).ReturnsAsync(returnObject);
            _service = _serviceMock.Object;

            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull(result); 
        }
    }
}