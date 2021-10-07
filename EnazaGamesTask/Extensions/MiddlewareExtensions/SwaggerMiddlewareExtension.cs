using Microsoft.AspNetCore.Builder;

namespace EnazaGamesTask.Extensions.MiddlewareExtensions
{
    public static class SwaggerMiddlewareExtension
    {
        public static void UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnazaGamesTask v1"));
        }
    }
}