using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace SampleAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly ILog _logger;
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public LoggingMiddleware(RequestDelegate next, ILog logger)
        {
            _next = next;
            _logger = logger;
        }

        private async Task Log(HttpContext context)
        {
            var existingResponse = context.Response.Body;
            var responseBodyStream = new MemoryStream();
            try
            {
                string path = context.Request.Path.Value;
                string queryString = context.Request.QueryString.Value;
                _logger.Info($"Request from {path} received.");

                var originalRequest = context.Request.Body;
                var requestBodyStream = new MemoryStream();

                await context.Request.Body.CopyToAsync(requestBodyStream);
                requestBodyStream.Seek(0, SeekOrigin.Begin);

                var str = new StreamReader(requestBodyStream).ReadToEnd();
                if (!string.IsNullOrEmpty(str))
                    _logger.Info($"Request body: {str}");

                if (!string.IsNullOrEmpty(queryString))
                {
                    var dict = QueryHelpers.ParseQuery(queryString);
                    var json = JsonConvert.SerializeObject(dict);
                    _logger.Info($"QueryString: {json}");
                }

                requestBodyStream.Seek(0, SeekOrigin.Begin);
                context.Request.Body = requestBodyStream;

                context.Response.Body = responseBodyStream;

                await _next(context);

                context.Request.Body = originalRequest;

                _logger.Info($"Response from {path} received.");
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                str = new StreamReader(responseBodyStream).ReadToEnd();

                if (!string.IsNullOrEmpty(str))
                    _logger.Info($"Response body: {str}");

                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(existingResponse);
            }
            catch (Exception ex)
            {
                var response = new ExceptionResponse(ex.Message);
                _logger.Error($"Unhandled Exception at {context.Request.Path.Value}: {ex.Message}");
                _logger.Error($"StackTrace: {ex.StackTrace}");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                byte[] messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                await responseBodyStream.WriteAsync(messageBytes, 0, messageBytes.Length);
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(existingResponse);
            }
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            //Request
            var request = httpContext.Request;
            var swaggerOrRoot = (request.Path.Value.Contains("swagger") || (request.Path.Value == "/") || (request.Path.Value.Contains("DocumentAsFile")) || (request.Path.Value.Contains("DocumentAsBytes")) || (request.Path.Value.Contains("DocumentAsStream")));

            if (!swaggerOrRoot)
            {
                await Log(httpContext);
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

    /// <summary>
    /// ExceptionResponse
    /// </summary>
    public class ExceptionResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="err"></param>
        public ExceptionResponse(string err)
        {
            ErrorMessage = err;
            Status = 500;
        }
        /// <summary>
        /// errorMessage
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public int Status { get; set; }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    /// <summary>
    /// LoggingMiddlewareExtensions
    /// </summary>
    public static class LoggingMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
