using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EnazaGamesTask.Extensions.ServiceExtensions
{
    public static class SwaggerServiceExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnazaGamesTask", Version = "v1" });
            });
        }
    }
}