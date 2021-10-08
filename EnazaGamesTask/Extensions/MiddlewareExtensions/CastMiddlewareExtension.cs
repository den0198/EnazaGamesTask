using EnazaGamesTask.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EnazaGamesTask.Extensions.MiddlewareExtensions
{
    public static class CastMiddlewareExtension
    {
        public static void AddCastMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}