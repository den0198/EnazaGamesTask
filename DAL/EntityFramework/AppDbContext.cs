using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.EntityFramework
{
    public class AppDbContext : IdentityDbContext<User, UserGroup, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        
        #region Entities

        private DbSet<UserState> UserStates { get; set; }

        #endregion
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var applicationContextAssembly = typeof(AppDbContext).Assembly;

            builder.Ignore<IdentityUserRole<int>>();
            
            builder.ApplyConfigurationsFromAssembly(applicationContextAssembly);
            
        }
    }
}