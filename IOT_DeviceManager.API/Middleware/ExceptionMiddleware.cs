using System;
using System.Net;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Helpers.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace IOT_DeviceManager.API.Middleware
{
    /// <summary>
    /// Middle ware that catches all exceptions that thrown.
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// The next delegate to be invoked.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The hosting environment. (Is used to check whether the environment is in development)
        /// </summary>
        private readonly IWebHostEnvironment _env;


        /// <summary>
        /// Constructs a new instance of the <see cref="ExceptionMiddleware"/>
        /// </summary>
        /// <param name="next">The next delegate to be invoked.</param>
        /// <param name="env">The hosting environment. (Is used to check whether the environment is in development)</param>
        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException e)
            {
                context.Response.StatusCode = e.StatusCode;

                var exceptionMessage = e.ClientMessage;
                if (!string.IsNullOrEmpty(e.Message))
                {
                    exceptionMessage = e.Message;
                }

                await context.Response.WriteAsync(exceptionMessage);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync("An unexpected fault happened.");

                if (_env.IsDevelopment())
                    await context.Response.WriteAsync($"\r\n\r\n{e.Message}");
            }
        }
    }
}
