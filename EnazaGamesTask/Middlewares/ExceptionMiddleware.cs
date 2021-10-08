using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models.DTOs.Requests;

namespace EnazaGamesTask.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {   
                context.Response.ContentType = MediaTypeNames.Application.Json;
                const int STATUS = (int)HttpStatusCode.BadRequest;
                context.Response.StatusCode = STATUS;
                var model = new BadRequest
                {
                    Status = STATUS,
                    Errors = e.Message.Split("\n").ToList()
                };
                
                await context.Response.WriteAsync(JsonSerializer.Serialize(model));
            }
        }
    }
}