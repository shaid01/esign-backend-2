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

    //    Header:
    //        {
    //            "alg": "HS256",
		  //"typ": "JWT"

    //    }

    //    Payload:
    //        {
    //            "nameid": "340",
		  //"unique_name": "igorz",
		  //"role": "מנהל",
		  //"nbf": 1749986224,
		  //"exp": 1749993424,
		  //"iat": 1749986224

    //    }

    //    Signature:
    //        9bQSushwr_FGbcNs1S6Muhk0f9BnRV4OrcSH5vO34Bs

            var authHeaderExist = !string.IsNullOrEmpty(context.Request.Headers["Authorization"]);

            // has no effect on the given call (the only useful outcome is investigating token structure)

            if (!authHeaderExist && tokenCookie != null)
            {
                //context.Request.Headers.Add("Authorization", $"Bearer: {tokenCookie}");
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
