using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace ASimpleBlogStarter.Server.Plumbing
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public List<string> Errors { get; set; }

        public RestException(HttpStatusCode statusCode, string error)
        {
            StatusCode = statusCode;
            Errors = new List<string> {error};
        }
    }

    public class HandleErrorsMiddleware
    {
        private readonly RequestDelegate _next;

        public HandleErrorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private string result;

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            if (!path.Value.StartsWith("/api"))
            {
                await _next(context);               
            } else
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    switch (ex)
                    {
                        case RestException re:
                            {
                                context.Response.StatusCode = (int)re.StatusCode;
                                result = JsonSerializer.Serialize(new { re.Errors });
                                break;
                            }
                    }

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result ?? "{}");
                }               
            }           
        }
    }
}