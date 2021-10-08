using DAL.EntityFramework;
using EnazaGamesTask.Tests.Infrastructure.Moсks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public class DbContextHelper
    {
        public DbContextHelper()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder
                .UseInMemoryDatabase("EnazaGamesTask.Tests1")
                .ConfigureWarnings(x => 
                    x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            var options = builder.Options;
            Context = new AppDbContext(options);
            
            Context.AddRange(UserMock.GetMany());

            Context.SaveChanges();
        }

        public AppDbContext Context { get; set; }
    }
}