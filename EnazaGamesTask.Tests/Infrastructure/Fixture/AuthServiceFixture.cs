using System.Linq;
using BLL.Services;
using EnazaGamesTask.Tests.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Models.Options;
using Moq;

namespace EnazaGamesTask.Tests.Infrastructure.Fixture
{
    public class AuthServiceFixture
    {
        public AuthService Create()
        {
            var users = MockDbContextHelper.GetInMemoryContext().Users
                .ToList();
            
            var userManager = UserManagerHelper.GetMock(users).Object;
            var authOptions = new Mock<IOptions<AuthOptions>>();
            
            return new AuthService(userManager,authOptions.Object);
        }
    }
    
}