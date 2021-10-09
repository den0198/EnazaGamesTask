using DAL.EntityFramework;
using EnazaGamesTask.Tests.Infrastructure.Helpers;

namespace EnazaGamesTask.Tests.Infrastructure.Fixture
{
    public class DbContextFixture
    {
        public AppDbContext Create() =>
            MockDbContextHelper.GetInMemoryContext();
    }
}