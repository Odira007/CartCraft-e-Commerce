using CartCraft.Models.DTOs.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartCraft.API.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogger logger)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);
                await HandleUncaughtException(context, ex);
            }
        }

        public async Task HandleUncaughtException(HttpContext context, Exception ex)
        {
            var response = context.Response;
            var responseModel = ApiResponse.Failure("Something went wrong");

            switch(ex)
            {
                case ArgumentNullException e:
                    response.StatusCode = (int)StatusCodes.Status404NotFound;
                    responseModel.Message = "Please pass in valid input";
                    break;
                case NullReferenceException e:
                    response.StatusCode = (int)StatusCodes.Status400BadRequest;
                    break;
                default:
                    response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                    break;
            }

            var errorResult = JsonConvert.SerializeObject(responseModel);
            await response.WriteAsync(errorResult);
        }
    }
    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
