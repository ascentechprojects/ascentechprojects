using app.advertise.DataAccess.Entities.Vendor;
using app.advertise.DataAccess.Repositories.Vendor;
using app.advertise.dtos.Vendor;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.services.Interfaces;
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
        private readonly IEmailService _emailService;
        public oAuthService(ICitizenRepository repository, IDataProtectionProvider dataProtector, DataProtectionPurpose dataProtectionPurpose, ILogger<oAuthService> logger, VendorRequestHeaders authData, IEmailService emailService)
        {
            _repository = repository;
            _logger = logger;
            _authData = authData;
            _recordDataProtector = dataProtector.CreateProtector(dataProtectionPurpose.RecordIdRouteValue);
            _emailService = emailService;
        }

        public async Task<dtoCitizenLoginResponse> VerifyCitizen(dtoCitizenLoginRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_userid", request.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_password", new Sha256Encryptor(request.SecretKey, _logger).EncrptedData, DbType.String, ParameterDirection.Input);
            parameters.Add("in_ulbID", request.UlbId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_ipaddr", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_source", _authData.Source, DbType.String, ParameterDirection.Input);

            var response = await _repository.VerifyCitizen(parameters);

            if (response == null || !(response.NUM_CITIZENUSER_ULBID > 0) || string.IsNullOrEmpty(response.VAR_CORPORATION_NAME) || string.IsNullOrEmpty(response.VAR_CORPORATION_ADDRESS) || !(response.NUM_CITIZENUSER_USERID > 0))
                throw new ApiException("User Not found.", _logger);


            return new dtoCitizenLoginResponse()
            {
                UlbId = _recordDataProtector.Protect(response.NUM_CITIZENUSER_ULBID.ToString()),
                UserId = _recordDataProtector.Protect(response.NUM_CITIZENUSER_USERID.ToString()),
                UserName = response.VAR_CITIZENUSER_NAME,
                OrgAddress = response.VAR_CORPORATION_ADDRESS,
                OrgName = response.VAR_CORPORATION_NAME,
                ReqToken = Guid.NewGuid().ToString(),
                UserType = UserTypeEnum.Citizen.ToString()
            };
        }

        public async Task<dtoCitizen> RegisterCitizen(dtoCitizen request)
        {
            var user = new CitizenUser()
            {
                VAR_CITIZENUSER_EMAILID = request.EmailId.Trim().ToLower(),
                NUM_CITIZENUSER_ULBID = request.UlbId
            };

            var existingUser = await _repository.VerifyUserEmail(user);

            if (existingUser != null && !string.IsNullOrEmpty(existingUser.VAR_CITIZENUSER_EMAILID) && existingUser.NUM_CITIZENUSER_USERID > 0)
                throw new ApiException($"User {request.EmailId} is already exists.", _logger);


            var parameters = new DynamicParameters();
            parameters.Add("in_ulbID", request.UlbId);
            parameters.Add("in_userid", null);
            parameters.Add("in_fname", request.FName);
            parameters.Add("in_mname", request.MName);
            parameters.Add("in_lname", request.LName);
            parameters.Add("in_address", request.Address);
            parameters.Add("in_dob", request.DateOfBirth);
            parameters.Add("in_emailid", request.EmailId.Trim().ToLower());
            parameters.Add("in_mobnumber", request.MobileNo);
            parameters.Add("IN_Gender", request.Gender);
            parameters.Add("in_domainlink", null);
            parameters.Add("In_AdharNo", request.AadharNo);
            parameters.Add("in_ipaddress", _authData.IpAddress);
            parameters.Add("in_source", _authData.Source);
            
            var result = await _repository.RegisterCitizen(parameters);

            if (result == null || string.IsNullOrEmpty(result.VAR_CITIZENUSER_EMAILLINK))
                throw new ApiException("Process got failed, please try again.", _logger);

            var userExists = await _repository.VerifyUserEmail(user);

            if (userExists == null && !string.Equals(userExists.VAR_CITIZENUSER_EMAILID, request.EmailId.Trim(), StringComparison.OrdinalIgnoreCase) && userExists.NUM_CITIZENUSER_USERID > 0 && userExists.NUM_CITIZENUSER_ULBID != user.NUM_CITIZENUSER_ULBID)
                throw new ApiException($"Unable to find user {request.EmailId}.", _logger);

            userExists.NUM_CITIZENUSER_OTP = StaticHelpers.Random4Digits();
            _repository.UpdateUserOTP(userExists);

            await _emailService.SendEmailAsync(new dtos.dtoEmailBody()
            {
                To = userExists.VAR_CITIZENUSER_EMAILID,
                Subject = "OTP for Registration",
                Body = $@"Please use the following One-Time Password (OTP) to authenticate: {userExists.NUM_CITIZENUSER_OTP}. Remember, this OTP is time-limited and should be kept private. Ignore if you didn't request it"
            });

            return new dtoCitizen()
            {
                EmailLink = result.VAR_CITIZENUSER_EMAILLINK,
                User = _recordDataProtector.Protect(userExists.NUM_CITIZENUSER_USERID.ToString()),
                EmailId = userExists.VAR_CITIZENUSER_EMAILID,
                P_UlbId = _recordDataProtector.Protect(userExists.NUM_CITIZENUSER_ULBID.ToString())
            };
        }

        public async Task<dtoOTPPasswordResponse> OtpWithResetPassword(dtoOTPPasswordReset dto)
        {

            var userId = Convert.ToInt32(_recordDataProtector.Unprotect(dto.UserId));

            if (!(userId > 0))
                throw new ApiException("Invalid User request", _logger);

            var ulbId = Convert.ToInt32(_recordDataProtector.Unprotect(dto.UlbId));
            if (!(ulbId > 0))
                throw new ApiException("Invalid Ulb request", _logger);

            var otpResult = _repository.VerifyUserOTP(new CitizenUser()
            {
                NUM_CITIZENUSER_USERID = userId,
                VAR_CITIZENUSER_EMAILID = dto.UserEmailId.ToLower(),
                NUM_CITIZENUSER_OTP = dto.OTP,
                NUM_CITIZENUSER_ULBID = ulbId
            });

            if (otpResult == null || !string.Equals(dto.OTP, otpResult.NUM_CITIZENUSER_OTP) || !string.Equals(userId, otpResult.NUM_CITIZENUSER_USERID) || !string.Equals(ulbId, otpResult.NUM_CITIZENUSER_ULBID))
                throw new ApiException("Invalid reset password request. Please contact the admin for any assistance.", _logger);


            otpResult.VAR_CITIZENUSER_PASSWORD = new Sha256Encryptor(dto.ConfirmPassword, _logger).EncrptedData;
            otpResult.VAR_CITIZENUSER_EMAILID = dto.UserEmailId.ToLower();
            await _repository.UpdateUserPassword(otpResult);

            return new dtoOTPPasswordResponse() { PasswordReset = true };
        }
    }
}
