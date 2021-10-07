using EnazaGamesTask.Extensions.MiddlewareExtensions;
using EnazaGamesTask.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnazaGamesTask
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            #region Infrastructure

            services.AddControllers();
            services.AddSwagger();

            #endregion
            
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Infrastructure
            
            app.UseAppSwagger();
            app.UseHttpsRedirection();
            app.UseAppRouting();
            app.UseAuthorization();
            
            #endregion
            
        }
    }
}