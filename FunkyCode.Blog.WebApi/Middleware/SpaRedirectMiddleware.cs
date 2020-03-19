using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FunkyCode.Blog.App.Internals;
using Microsoft.AspNetCore.Http;

namespace FunkyCode.Blog.Inf.WebApi
{
    public class SpaRedirectMiddleware
    {
        private readonly RequestDelegate _next;
        

        public SpaRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
            
            if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
            {
                context.Request.Path = "/index.html";
                context.Response.StatusCode = 200;
              
                await _next(context);
            }
            
        }
    }
   
}
