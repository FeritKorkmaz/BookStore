using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.MiddleWares
{
    public class CustomExeptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExeptionMiddleWare(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke (HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                
                string message = "(Request)  HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message);

                await _next(context);
                watch.Stop();

                message = "(Response) HTTP " + context.Request.Method + " - " + context.Request.Path + " responed " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                _loggerService.Write(message);                
            }
            catch (Exception ex)
            {
                watch.Stop();                
                await HandleExeption(context, ex, watch);
            }
        
        }

        private Task HandleExeption(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;

            string message = "(Error)    HTTP " + context.Request.Method + " - " + "responed " + context.Response.StatusCode + "Error Message: " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);            

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            return context.Response.WriteAsync(result);          
        }
    }

    public static class CustomExeptionMiddleWareExtensions
    {
        public static IApplicationBuilder UseCustomExeptionMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExeptionMiddleWare>();
        }
    }
}