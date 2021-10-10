using System.Linq;
using DAL.EntityFramework;
using EnazaGamesTask.Tests.Infrastructure.Moсks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Models.Entities;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public class DbContextHelper
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public DbContextHelper()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder
                .UseInMemoryDatabase("EnazaGamesTask.Tests")
                .ConfigureWarnings(x => 
                    x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            builder.UseLazyLoadingProxies();
            
            _options = builder.Options;
            
            #region AddMoks
            
            using var context = new AppDbContext(_options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            context.AddRange(UserGroupMock.GetMany());
            context.AddRange(UserStateMock.GetMany());
            context.AddRange(UserMock.GetMany());
            
            context.SaveChanges();

            var users = context.Users
                .Include(x => x.UserGroup)
                .Include(x => x.UserState)
                .ToList();;
            
            UserManager = UserManagerHelper.GetMock(users).Object;
            RoleManager = RoleManagerHelper.GetMock(users).Object;
            
            #endregion
            
        }

        public UserManager<User> UserManager { get; }
        public RoleManager<UserGroup> RoleManager { get; }
        
        public AppDbContext Create() => new AppDbContext(_options);

    }
}