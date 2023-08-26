namespace Ads.Merchant.API.Configs;
using System.Net;
using Ads.Merchant.API.V1.Models;
using Microsoft.AspNetCore.Diagnostics;


public class ErrorHandlingSettings
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;
                var errorResponse = new ErrorResponse
                {
                    Message = GetErrorMessage(exception),
                    ExceptionMessage = exception?.Message
                };
                var jsonErrorResponse = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponse);
                return context.Response.WriteAsync(jsonErrorResponse);
            });
        });
    }
    private string GetErrorMessage(Exception exception)
    {
        if (exception is ArgumentNullException)
        {
            return "One or more required parameters are missing.";
        }
        else if (exception is InvalidOperationException)
        {
            return "An invalid operation occurred.";
        }
        else
        {
            return "An error occurred while processing your request.";
        }
    }
}
