using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using Todo.Minimalist.Api.Models.Errors;

namespace Todo.Minimalist.Api.Middlewares;

public static class ExceptionHandlingMiddleware
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(handlerApp =>
        {
            handlerApp.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = feature?.Error;

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                logger.LogError(exception, "Unhandled exception");

                var response = new ErrorResponse
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "An unexpected error occurred."
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            });
        });
    }
}