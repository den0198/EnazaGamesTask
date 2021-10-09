using System.Linq;
using EnazaGamesTask.Tests.Infrastructure.Fixture;
using EnazaGamesTask.Tests.Infrastructure.Helpers;
using Models.DTOs.Requests;
using Xunit;

namespace EnazaGamesTask.Tests.Services
{
    public class AuthServiceTests : IClassFixture<AuthServiceFixture>
    {
        private readonly AuthServiceFixture _fixture;

        public AuthServiceTests(AuthServiceFixture fixture)
        {
            _fixture = fixture;
        }
        
        
        [Fact]
        public async void ItShouldSignIlIfHaveUser()
        {
            //arrange
            var sut = _fixture.Create();

            //act
            var firstUser = MockDbContextHelper.GetInMemoryContext().Users.FirstOrDefault();
            var actual = await sut.SignIn(new AddUserRequest
            {
                Login = firstUser?.Login,
                Password = firstUser?.Password
            });

            //assert
            Assert.NotNull(actual);
        }
        
    }
}