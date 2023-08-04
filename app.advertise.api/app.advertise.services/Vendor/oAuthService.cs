using app.advertise.DataAccess.Repositories.Vendor;
using app.advertise.dtos.Vendor;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.services.Vendor.Interfaces;
using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Vendor
{
    public class oAuthService : IoAuthService
    {
        private readonly IoAuthRepository _repository;
        private readonly ILogger<oAuthService> _logger;
        private readonly VendorRequestHeaders _authData;
        private readonly IDataProtector _recordDataProtector;
        public oAuthService(IoAuthRepository repository, IDataProtectionProvider dataProtector, DataProtectionPurpose dataProtectionPurpose, ILogger<oAuthService> logger, VendorRequestHeaders authData)
        {
            _repository = repository;
            _logger = logger;
            _authData = authData;
            _recordDataProtector = dataProtector.CreateProtector(dataProtectionPurpose.RecordIdRouteValue);
        }

        public async Task<dtoCitizenLoginResponse> VerifyCitizen(dtoCitizenLoginRequest request)
        {
            //use pass encryption
            var parameters = new DynamicParameters();
            parameters.Add("in_userid", request.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_password", request.SecretKey, DbType.String, ParameterDirection.Input);
            parameters.Add("in_ulbID", request.UlbId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_ipaddr", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_source", _authData.Source, DbType.String, ParameterDirection.Input);

            var response = await _repository.VerifyCitizen(parameters);

            if (response == null||!(response.NUM_CITIZENUSER_ULBID>0)||string.IsNullOrEmpty(response.VAR_CORPORATION_NAME) || string.IsNullOrEmpty(response.VAR_CORPORATION_ADDRESS) || !(response.NUM_CITIZENUSER_USERID>0))
                throw new ApiException("User Not found.", _logger);

            //assume user role and usertype is vendor


            return new dtoCitizenLoginResponse()
            {
                UlbId= _recordDataProtector.Protect(response.NUM_CITIZENUSER_ULBID.ToString()),
                UserId= _recordDataProtector.Protect(response.NUM_CITIZENUSER_USERID.ToString()),
                UserName=response.VAR_CITIZENUSER_NAME,
                OrgAddress=response.VAR_CORPORATION_ADDRESS,
                OrgName=response.VAR_CORPORATION_NAME,
                ReqToken=Guid.NewGuid().ToString(),
            };
        }
    }
}
