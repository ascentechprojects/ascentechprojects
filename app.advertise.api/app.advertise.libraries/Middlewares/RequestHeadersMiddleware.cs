using Microsoft.AspNetCore.Http;

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
            string userIpAddress = context.Request.Headers[AppConstants.Header_IPAddress];

            _userRequestHeaders.IpAddress=userIpAddress;

            await _next(context);
        }
    }
}
