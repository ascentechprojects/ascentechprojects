using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.advertise.libraries.Middlewares
{
    public class RequestHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserRequestHeaders _userRequestHeaders;
        public RequestHeadersMiddleware(RequestDelegate next, UserRequestHeaders userRequestHeaders)
        {
            _next = next;
            _userRequestHeaders = userRequestHeaders;

        }

        public async Task Invoke(HttpContext context)
        {
            string userIpAddress = context.Request.Headers["UserIpAddress"];

            _userRequestHeaders.IpAddress=userIpAddress;

            await _next(context);
        }
    }
}
