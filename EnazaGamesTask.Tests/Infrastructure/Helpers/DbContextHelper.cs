using DAL.EntityFramework;
using EnazaGamesTask.Tests.Infrastructure.Moсks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Models.Entities;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public class DbContextHelper : IdentityDbContext<User, UserGroup, int>
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

            #region AddMoks

            Context.AddRange(UserGroupMock.GetMany());
            Context.AddRange(UserStateMock.GetMany());
            Context.AddRange(UserMock.GetMany());

            #endregion
            
            Context.SaveChanges();
        }

        public AppDbContext Context { get; set; }
    }
}