using app.advertise.libraries.Exceptions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace app.advertise.libraries.Middlewares
{
    public class RequestHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserRequestHeaders _userRequestHeaders;
        private readonly VendorRequestHeaders _citizenRequestHeaders;
        private readonly ILogger<RequestHeadersMiddleware> _logger;
        private readonly IDataProtector _adminDataProtector;
        public RequestHeadersMiddleware(RequestDelegate next, UserRequestHeaders userRequestHeaders, ILogger<RequestHeadersMiddleware> logger, DataProtectionPurpose dataProtectionPurpose, IDataProtectionProvider adminDataProtector, VendorRequestHeaders citizenRequestHeaders)
        {
            _next = next;
            _userRequestHeaders = userRequestHeaders;
            _logger = logger;
            _adminDataProtector = adminDataProtector.CreateProtector(dataProtectionPurpose.AdminAuthValue);
            _citizenRequestHeaders = citizenRequestHeaders;

        }

        public async Task Invoke(HttpContext context)
        {
            var userIpAddress = context.Request.Headers[AppConstants.Header_IPAddress];
            if (context.Request.Headers.ContainsKey(AppConstants.Header_ULB))
            {
                var ulb = context.Request.Headers[AppConstants.Header_ULB];
                _userRequestHeaders.UlbId= string.IsNullOrEmpty(ulb) ? throw new ApiException("Invalid Ulb header", _logger) : !(Convert.ToInt32(_adminDataProtector.Unprotect(ulb)) >0)? throw new ApiException("Invalid Ulb header", _logger): Convert.ToInt32(_adminDataProtector.Unprotect(ulb));
            }

            if (context.Request.Headers.ContainsKey(AppConstants.Header_User))
            {
                var user = context.Request.Headers[AppConstants.Header_User];
                _userRequestHeaders.UserId = string.IsNullOrEmpty(user) ? throw new ApiException("Invalid User header", _logger) : _adminDataProtector.Unprotect(user);
            }

            _userRequestHeaders.IpAddress= context.Connection.RemoteIpAddress.ToString()?? userIpAddress;
            _userRequestHeaders.HostAddress= context.Request.Host.Value;

            _citizenRequestHeaders.IpAddress = context.Connection.RemoteIpAddress.ToString() ?? userIpAddress;
            _citizenRequestHeaders.HostAddress = context.Request.Host.Value;

            if (context.Request.Headers.ContainsKey(AppConstants.Header_Vendor_ULB))
            {
                var ulb = context.Request.Headers[AppConstants.Header_Vendor_ULB];
                _citizenRequestHeaders.UlbId = string.IsNullOrEmpty(ulb) ? throw new ApiException("Invalid Ulb header", _logger) : !(Convert.ToInt32(_adminDataProtector.Unprotect(ulb)) > 0) ? throw new ApiException("Invalid Ulb header", _logger) : Convert.ToInt32(_adminDataProtector.Unprotect(ulb));
            }


            await _next(context);
        }
    }
}
