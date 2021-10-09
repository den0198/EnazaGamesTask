using Microsoft.Extensions.Options;
using Models.Options;
using Moq;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public static class AuthOptionHelper
    {
        public static Mock<IOptions<AuthOptions>> GetMock()
        {
            var mrg = new Mock<IOptions<AuthOptions>>();

            mrg.Setup(x => x.Value)
                .Returns(new AuthOptions
                {
                    Audience = "Test",
                    Issuer = "Test",
                    Key = "d72e52de-2e90-42h9-g80a-34cc91a29169",
                    Lifetime = 10
                });
            
            return mrg;
        }
    }
}