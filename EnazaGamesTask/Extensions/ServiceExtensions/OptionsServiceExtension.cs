using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Options;

namespace EnazaGamesTask.Extensions.ServiceExtensions
{
    public static class OptionsServiceExtension
    {
        public static void AddAllOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));
        }
    }
}