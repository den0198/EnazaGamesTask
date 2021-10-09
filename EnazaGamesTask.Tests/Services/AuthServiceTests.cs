using EnazaGamesTask.Tests.Infrastructure.Fixture;
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
        public async void ItShouldUserFirstUserStateNotNul()
        {
            //arrange
            var sut = _fixture.Create();

            //act
            var actual = await sut.SignIn(new AddUserRequest
            {
                Login = "TestUser",
                Password = "qwe123QWE!@#"
            });

            //assert
            Assert.NotNull(actual);
        }
        
    }
}