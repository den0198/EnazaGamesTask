using DAL.EntityFramework;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public static class MockDbContextHelper
    {
        private static readonly DbContextHelper dbContextHelper = new DbContextHelper();
        
        public static AppDbContext GetInMemoryContext() =>
            dbContextHelper.Context;
    }
}