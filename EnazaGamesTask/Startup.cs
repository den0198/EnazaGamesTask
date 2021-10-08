using EnazaGamesTask.Extensions.MiddlewareExtensions;
using EnazaGamesTask.Extensions.ServiceExtensions;
using EnazaGamesTask.Middlewares;
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
            services.AddEntityFramework(_configuration);
            services.AddAuth(_configuration);
            services.AddAllOptions(_configuration);
            services.AddBllServices();
            services.AddSwagger();
            
            #endregion

        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.AddCastMiddlewares();
            
            #region Infrastructure
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuth();
            app.UseAppEndpoints();
            app.UseAppSwagger();
            
            #endregion
            
            app.InitializationDatabase();
        }
    }
}