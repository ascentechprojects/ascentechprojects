using app.advertise.DataAccess.Entities.Vendor;
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
        private readonly ICitizenRepository _repository;
        private readonly ILogger<oAuthService> _logger;
        private readonly VendorRequestHeaders _authData;
        private readonly IDataProtector _recordDataProtector;
        public oAuthService(ICitizenRepository repository, IDataProtectionProvider dataProtector, DataProtectionPurpose dataProtectionPurpose, ILogger<oAuthService> logger, VendorRequestHeaders authData)
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

            if (response == null || !(response.NUM_CITIZENUSER_ULBID > 0) || string.IsNullOrEmpty(response.VAR_CORPORATION_NAME) || string.IsNullOrEmpty(response.VAR_CORPORATION_ADDRESS) || !(response.NUM_CITIZENUSER_USERID > 0))
                throw new ApiException("User Not found.", _logger);

            //assume user role and usertype is vendor


            return new dtoCitizenLoginResponse()
            {
                UlbId = _recordDataProtector.Protect(response.NUM_CITIZENUSER_ULBID.ToString()),
                UserId = _recordDataProtector.Protect(response.NUM_CITIZENUSER_USERID.ToString()),
                UserName = response.VAR_CITIZENUSER_NAME,
                OrgAddress = response.VAR_CORPORATION_ADDRESS,
                OrgName = response.VAR_CORPORATION_NAME,
                ReqToken = Guid.NewGuid().ToString(),
            };
        }

        public async Task<dtoCitizen> RegisterCitizen(dtoCitizen request)
        {
            var user = new CitizenUser()
            {
                VAR_CITIZENUSER_EMAILID = request.EmailId.Trim()
            };

            var existingUser = await _repository.VerifyUserEmail(user);

            if (existingUser != null && !string.IsNullOrEmpty(existingUser.VAR_CITIZENUSER_EMAILID) && existingUser.NUM_CITIZENUSER_USERID > 0)
                throw new ApiException($"User {request.EmailId} is already exists.", _logger);

            var parameters = new DynamicParameters();
            parameters.Add("in_ulbID", request.UlbId, DbType.Int32);
            parameters.Add("in_userid", 0);
            parameters.Add("in_fname", request.FName, DbType.String);
            parameters.Add("in_mname", request.MName, DbType.String);
            parameters.Add("in_lname", request.LName, DbType.String);
            parameters.Add("in_address", request.Address, DbType.String);
            parameters.Add("in_dob", request.DateOfBirth, DbType.Date);
            parameters.Add("in_emailid", request.EmailId.Trim(), DbType.String);
            parameters.Add("in_mobnumber", request.MobileNo, DbType.Int64);
            parameters.Add("IN_Gender", request.Gender, DbType.String);
            parameters.Add("in_domainlink", null, DbType.String);
            parameters.Add("In_AdharNo", request.AadharNo, DbType.String);
            parameters.Add("in_ipaddress", _authData.IpAddress, DbType.String);
            parameters.Add("in_source", _authData.Source, DbType.String);

            var result = await _repository.RegisterCitizen(parameters);

            if (result == null || string.IsNullOrEmpty(result.VAR_CITIZENUSER_EMAILLINK))
                throw new ApiException("Process got failed, please try again.", _logger);

            return new dtoCitizen()
            {
                EmailLink = result.VAR_CITIZENUSER_EMAILLINK
            };
        }
    }
}
