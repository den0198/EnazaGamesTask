using BLL.Services;
using EnazaGamesTask.Tests.Infrastructure.Helpers;
using Xunit;

namespace EnazaGamesTask.Tests.Services
{
    public class UserServiceTests 
    {
        private readonly DbContextHelper _db; 

        public UserServiceTests()
        {
            _db = new DbContextHelper();
        }
        
        [Fact]
        public async void ItShouldGetAllUser()
        {
            //arrange
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act
            var actual = await sut.GetAllUsers();

            //assert
            Assert.NotNull(actual);
        }
    }
}