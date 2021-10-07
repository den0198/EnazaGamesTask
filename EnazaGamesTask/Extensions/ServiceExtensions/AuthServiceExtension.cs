﻿using Common.HelpersClasses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models.Options;

namespace EnazaGamesTask.Extensions.ServiceExtensions
{
    public static class AuthServiceExtension
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();
            
            addAuthentication(services, authOptions);
        }

        private static void addAuthentication(IServiceCollection services, AuthOptions authOptions) =>
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = authOptions.Issuer,
                        ValidateIssuer = true,
                        ValidAudience = authOptions.Audience,
                        ValidateAudience = true,
                        IssuerSigningKey = AuthHelper.GetIssuerSigningKey(authOptions.Key),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true
                    });

    }
}