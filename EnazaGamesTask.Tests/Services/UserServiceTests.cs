using EnazaGamesTask.Tests.Infrastructure.Fixture;
using Xunit;

namespace EnazaGamesTask.Tests.Services
{
    public class UserServiceTests : IClassFixture<UserServiceFixture>
    {
        private readonly UserServiceFixture _fixture;

        public UserServiceTests(UserServiceFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public async void ItShouldGetAllUser()
        {
            //arrange
            var sut = _fixture.Create();

            //act
            var actual = await sut.GetAllUsers();

            //assert
            Assert.NotNull(actual);
        }
    }
}