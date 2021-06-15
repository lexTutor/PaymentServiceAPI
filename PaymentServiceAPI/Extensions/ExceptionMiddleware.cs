using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentService.Application.Commons.CustomExceptions;
using PaymentService.Domain.Common;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PaymentServiceAPI.Extensions
{
    public class ExceptionMiddleware
    { 
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// ExceptionMiddleware constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Method to invoke our exception
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex.Message);
                await ConvertException(ex, context);
            }
        }


        //Determines the status code to return and the creates the response object in the event of an error.
        private async Task ConvertException(Exception exception, HttpContext context)

        {
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            httpStatusCode = exception switch
            {
                BadRequestException accessViolationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };

            context.Response.StatusCode = (int)httpStatusCode;
            var response = _env.IsDevelopment()
                    ? new Response<string>(exception.Message, exception.StackTrace?.ToString()).ToString()
                    : new Response<string>(exception.Message).ToString();

            await context.Response.WriteAsync(response);
        }
    }
}
