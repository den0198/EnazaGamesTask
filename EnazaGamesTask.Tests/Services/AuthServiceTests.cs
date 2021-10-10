using BLL.Services;
using EnazaGamesTask.Tests.Infrastructure.Helpers;
using EnazaGamesTask.Tests.Infrastructure.Moсks;
using Models.DTOs.Requests;
using Xunit;

namespace EnazaGamesTask.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly DbContextHelper _db;

        public AuthServiceTests()
        {
            _db = new DbContextHelper();
        }
        
        [Fact]
        public async void ItShouldSignIlIfHaveUser()
        {
            //arrange
            var authOptions = AuthOptionHelper.GetMock().Object;
            var sut = new AuthService(_db.UserManager,authOptions); 

            //act
            var user = UserMock.GetOne();
            var actual = await sut.SignIn(new AddUserRequest
            {
                Login = user!.Login,
                Password = user!.Password
            });

            //assert
            Assert.NotNull(actual);
        }
        
    }
}