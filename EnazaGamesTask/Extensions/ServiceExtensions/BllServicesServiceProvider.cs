using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EnazaGamesTask.Extensions.ServiceExtensions
{
    public static class BllServicesServiceProvider
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddTransient<AuthService>();
            services.AddTransient<UserService>();
        }
    }
}