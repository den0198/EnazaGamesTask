using EnazaGamesTask.Tests.Infrastructure.Helpers;
using Xunit;

namespace EnazaGamesTask.Tests1
{
    public class ContextTests : IClassFixture<DbContextHelper>
    {
        private readonly DbContextHelper _fixture;

        public ContextTests(DbContextHelper fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldContextNotNul()
        {
            //arrange
            var sut = _fixture.Context;
            
            //act
            
            //assert
            Assert.NotNull(sut);
        }
    }
}