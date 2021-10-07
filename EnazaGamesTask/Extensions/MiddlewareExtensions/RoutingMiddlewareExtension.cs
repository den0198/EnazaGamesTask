using Microsoft.AspNetCore.Builder;

namespace EnazaGamesTask.Extensions.MiddlewareExtensions
{
    public static class RoutingMiddlewareExtension
    {
        public static void UseAppRouting(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}