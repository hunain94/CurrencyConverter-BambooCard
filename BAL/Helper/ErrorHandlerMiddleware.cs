using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BAL.Helper
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                context.Response.ContentType = "application/json";
                var response = context.Response;
                ErrorDetails errorDetails = new ErrorDetails();

                switch (error)
                {
                    case ApplicationException ex:
                        errorDetails.responseCode = (int)HttpStatusCode.BadRequest;
                        errorDetails.responseMessage = error.Message;
                        break;
                    default:
                        errorDetails.responseCode = (int)HttpStatusCode.InternalServerError;
                        errorDetails.responseMessage = error.Message;
                        break;
                }

                var result = JsonSerializer.Serialize(errorDetails);
                await context.Response.WriteAsync(result);
            }
        }
    }
}
