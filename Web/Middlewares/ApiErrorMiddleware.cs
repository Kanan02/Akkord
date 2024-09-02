using Application.Exceptions;
using Application.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class ApiErrorMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ApiErrorMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            _logger = loggerFactory.CreateLogger<ApiErrorMiddleware>();
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                //LogRequestBody(context);
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.ToString());
            var code = HttpStatusCode.OK;

            var errorMsg = SetErrorKeywordByExType(ex);

            var result = JsonConvert.SerializeObject(
                new ApiResponse(errorMsg),  //new ApiResponse(ex.Message),
                 new JsonSerializerSettings
                 {
                     ContractResolver = new CamelCasePropertyNamesContractResolver()
                 });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private string SetErrorKeywordByExType(Exception ex) => ex is AkkordException ? ex.Message : "system_error";

        private async Task LogRequestBody(HttpContext context)
        {
            
                var bodyStr = "";
                var req = context.Request;

                // Allows using several time the stream in ASP.Net Core
                req.EnableBuffering();

                // Arguments: Stream, Encoding, detect encoding, buffer size 
                // AND, the most important: keep stream opened
                using (StreamReader reader
                          = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                }

                // Rewind, so the core is not lost when it looks the body for the request
                req.Body.Position = 0;

            // Do whatever work with bodyStr here
            _logger.LogInformation($"request body => {bodyStr}");



        }
    }
}
