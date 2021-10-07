using DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Options;

namespace EnazaGamesTask.Extensions.ServiceExtensions
{
    public static class EntityFrameworkServiceExtension
    {
        public static void AddEntityFramework
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();
            var passwordOptions = configuration.GetSection("PasswordOptions").Get<PasswordOptions>();
            
            addOptions(services, connectionString);
            addIdentity(services, authOptions, passwordOptions);
        }

        private static void addOptions(IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(connectionString);
            });
        

        private static void addIdentity(IServiceCollection services, AuthOptions authOptions, PasswordOptions passwordOptions) =>
            services.AddIdentityCore<User>(options =>
                    options.Password = passwordOptions)
                .AddRoles<UserGroup>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider(authOptions.Audience, typeof(DataProtectorTokenProvider<User>));

    }
}