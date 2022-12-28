using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EsignBackend.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private const string GENERAL_ERROR_CODE = "0";
        private readonly RequestDelegate _next;
        private readonly ILogger _log;


        public ErrorHandlingMiddleware(RequestDelegate next, ILogger log)
        {
            _next = next;
            _log = log;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string requestBody = null;
            try
            {

                requestBody = await FormatRequest(httpContext.Request);
                await _next(httpContext);
            }
            catch (InvalidOperationException ex)
            {
                // var generalError = new GeneralError(ex.Message);
                //_log.Error($"{Environment.NewLine}{Environment.NewLine}--------------{Environment.NewLine} Invalid Operation Error. Message: {string.Join("", generalError.errors["error"])}. Request: {requestBody}{Environment.NewLine}{ex.ToString()}");
                _log.Error(ex, "Validation error");

                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _log.Error($"{Environment.NewLine}{Environment.NewLine}--------------{Environment.NewLine} General Error. HResult : {ex.HResult}.  Request: {requestBody}{Environment.NewLine}{ex.ToString()}");
                //  var error = new GeneralError(GENERAL_ERROR_CODE, ex.HResult.ToString(), _dater.UtcNow());
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, string message,/*GeneralError error,*/ HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            //  var errorJson = JsonConvert.SerializeObject(error);  
            var errorJson = JsonConvert.SerializeObject(message);
            return context.Response.WriteAsync(errorJson);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;
            return $"{request.Method} - {request.Scheme}://{request.Host}{request.Path} {request.QueryString} {Environment.NewLine}{bodyAsText}";
        }

    }

    public static class ErrorHandlingMiddlewareExtentions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
