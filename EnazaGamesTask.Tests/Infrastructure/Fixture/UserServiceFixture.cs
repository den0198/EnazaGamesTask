using System.Linq;
using BLL.Services;
using EnazaGamesTask.Tests.Infrastructure.Helpers;

namespace EnazaGamesTask.Tests.Infrastructure.Fixture
{
    public class UserServiceFixture
    {
        public UserService Create()
        {
            var context = MockDbContextHelper.GetInMemoryContext();

            var users = context.Users.ToList();
            
            var userManager = UserManagerHelper.GetMock(users).Object;
            var roleManager = RoleManagerHelper.GetMock(users).Object;
            
            return new UserService(userManager,roleManager,context);
        }
    }
}