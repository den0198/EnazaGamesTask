using System.Linq;
using BLL.Services;
using EnazaGamesTask.Tests.Infrastructure.Helpers;

namespace EnazaGamesTask.Tests.Infrastructure.Fixture
{
    public class AuthServiceFixture
    {
        public AuthService Create()
        {
            var users = MockDbContextHelper.GetInMemoryContext().Users
                .ToList();
            
            var userManager = UserManagerHelper.GetMock(users).Object;
            var authOptions = AuthOptionHelper.GetMock().Object;
            
            return new AuthService(userManager,authOptions);
        }
    }
    
}