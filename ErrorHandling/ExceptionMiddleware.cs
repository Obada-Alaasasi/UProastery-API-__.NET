using Newtonsoft.Json;
using System.Net;
using UProastery_API.Exceptions.Exceptions;

namespace UProastery_API.Exceptions
{
    public class ExceptionMiddleware {

        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger) {
            try {
                await _next(context);
            }
            catch(Exception ex) {
                await HandleExceptions(context, logger, ex);
            }
        }

        public async Task HandleExceptions(HttpContext context, ILogger<ExceptionMiddleware> logger, Exception ex) {

            //log exception
            logger.LogError(ex.Message);

            //create a default error obj
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var error = new ErrorDetails() {
                ErrorMessage = $"{ex.Message}",
                ErrorType = "Failure"
            };

            //check error type
            switch (ex) {
                case NotFoundException notFoundExceptions:
                    statusCode = HttpStatusCode.NotFound;
                    error.ErrorType = "Not found";
                    break;
                default:
                    break;
            }

            //write response
            context.Response.StatusCode = (int)statusCode;
            var oResponse = JsonConvert.SerializeObject(error);
            await context.Response.WriteAsync(oResponse);
        }





    }
}
