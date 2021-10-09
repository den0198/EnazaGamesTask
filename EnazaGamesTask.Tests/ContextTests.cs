using System.Linq;
using EnazaGamesTask.Tests.Infrastructure.Fixture;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EnazaGamesTask.Tests1
{
    public class ContextTests : IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;

        public ContextTests(DbContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldContextNotNul()
        {
            //arrange
            var sut = _fixture.Create();
            
            //act

            //assert
            Assert.NotNull(sut);
        }
        
        [Fact]
        public void ItShouldUserFirstNotNul()
        {
            //arrange
            var sut = _fixture.Create();
            
            //act
            var actual = sut.Users.FirstOrDefault();
            
            //assert
            Assert.NotNull(actual);
        }
        
        [Fact]
        public void ItShouldUserFirstUserGroupNotNul()
        {
            //arrange
            var sut = _fixture.Create();

            //act
            var users = sut.Users
                .Include(x => x.UserGroup)
                .FirstOrDefault();
            var actual = users?.UserGroup;
            
            //assert
            Assert.NotNull(actual);
        }
        
        [Fact]
        public void ItShouldUserFirstUserStateNotNul()
        {
            //arrange
            var sut = _fixture.Create();

            //act
            var users = sut.Users
                .Include(x => x.UserState)
                .FirstOrDefault();
            var actual = users?.UserState;
            
            //assert
            Assert.NotNull(actual);
        }
    }
}