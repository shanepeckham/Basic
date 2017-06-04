using System.Threading;
using Microsoft.AspNetCore.Builder;

namespace Web
{
    public class HttpContextBinding
    {
        static AsyncLocal<Microsoft.AspNetCore.Http.HttpContext>   _httpContext = new AsyncLocal<Microsoft.AspNetCore.Http.HttpContext>();

        public static Microsoft.AspNetCore.Http.HttpContext    Current => _httpContext.Value;

        public static void Configure(IApplicationBuilder builder)
        {
            builder.Use(async (httpContext, next) =>
            {
                _httpContext.Value = httpContext;
                await next();
            });
        }
    }
}