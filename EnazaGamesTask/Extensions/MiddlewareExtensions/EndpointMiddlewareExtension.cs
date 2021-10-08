using Microsoft.AspNetCore.Builder;

namespace EnazaGamesTask.Extensions.MiddlewareExtensions
{
    public static class EndpointMiddlewareExtension
    {
        public static void UseAppEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}