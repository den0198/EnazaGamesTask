using Microsoft.AspNetCore.Builder;

namespace EnazaGamesTask.Extensions.MiddlewareExtensions
{
    public static class AuthMiddlewareExtension
    {
        public static void UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}