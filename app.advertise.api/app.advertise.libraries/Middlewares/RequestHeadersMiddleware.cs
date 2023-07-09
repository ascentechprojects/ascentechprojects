using app.advertise.libraries.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace app.advertise.libraries.Middlewares
{
    public class RequestHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserRequestHeaders _userRequestHeaders;
        private readonly ILogger<RequestHeadersMiddleware> _logger;
        public RequestHeadersMiddleware(RequestDelegate next, UserRequestHeaders userRequestHeaders, ILogger<RequestHeadersMiddleware> logger)
        {
            _next = next;
            _userRequestHeaders = userRequestHeaders;
            _logger = logger;

        }

        public async Task Invoke(HttpContext context)
        {
            var userIpAddress = context.Request.Headers[AppConstants.Header_IPAddress];
            var ulb = context.Request.Headers[AppConstants.Header_ULB];
            var userid = context.Request.Headers[AppConstants.Header_User];


            _userRequestHeaders.IpAddress= context.Connection.RemoteIpAddress.ToString()?? userIpAddress;
            _userRequestHeaders.UlbId= string.IsNullOrEmpty(ulb) ? Convert.ToInt32(ulb) : 0;
            _userRequestHeaders.UserId= userid;
            _userRequestHeaders.HostAddress= context.Request.Host.Value;

            await _next(context);
        }
    }
}
