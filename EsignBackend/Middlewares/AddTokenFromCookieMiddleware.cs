using Azure.Core;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EsignBackend.Middlewares
{
    public class AddTokenFromCookieMiddleware
    {
        private readonly RequestDelegate next;

        public AddTokenFromCookieMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tokenCookie = context.Request.Cookies["accessToken"];
            var authHeaderExist = !string.IsNullOrEmpty(context.Request.Headers["Authorization"]);

            if (!authHeaderExist && tokenCookie != null)
            {
                context.Request.Headers.Add("Authorization", $"Bearer: {tokenCookie}");
            }

            await next(context);
        }

        //public Task Invoke(HttpContext context)
        //{
        //    var tokenCookie = context.Request.Cookies["accessToken"];

        //    if (tokenCookie != null)
        //    {
        //        context.Request.Headers.Add("Authorization", $"Bearer: {tokenCookie}");
        //    }

        //    var result = next(context);
        //    return result;
        //}
    }
}
