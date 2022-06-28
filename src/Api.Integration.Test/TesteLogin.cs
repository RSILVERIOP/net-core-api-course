using Xunit;

namespace Api.Integration.Test
{
    public class TesteLogin : BaseIntegration
    {
        [Fact]
        public async Task TesteDoToken()
        {
            await AddToken();
        }
    }
}