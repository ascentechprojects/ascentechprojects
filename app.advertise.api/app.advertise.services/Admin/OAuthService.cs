using app.advertise.DataAccess;
using app.advertise.dtos.Admin;
using app.advertise.libraries.Exceptions;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Admin
{
    public class OAuthService: IOAuthService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly ILogger<OAuthService> _logger;
        public OAuthService(IAdminUserRepository adminUserRepository, ILogger<OAuthService> logger)
        {
            _adminUserRepository = adminUserRepository;
            _logger = logger;
        }

        //to do claim,
        public async Task<dtoAuthResponse> AuthenticateUser(dtoAuthRequest authRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_UserId", authRequest.User);
            parameters.Add("in_password", authRequest.Password);
            parameters.Add("in_macaddr", "");
            parameters.Add("in_ipaddr", "");
            parameters.Add("in_hostname", "");
            parameters.Add("in_source", "Web");
            parameters.Add("in_deptid", "");

            parameters.Add("Out_UserName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            parameters.Add("Out_userid", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parameters.Add("Out_LastLogin", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parameters.Add("Out_LastLogOut", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parameters.Add("Out_Corporation", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            parameters.Add("Out_CorporationAddress", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            parameters.Add("Out_ReceiptOfficeName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            parameters.Add("Out_ChalanOfficeName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            parameters.Add("Out_PrabhagName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            parameters.Add("Out_PrabhagID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parameters.Add("Out_DesigID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parameters.Add("Out_UserType", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parameters.Add("Out_Collectioncenter", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("Out_mobileno", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);
            parameters.Add("Out_OtpValidate", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);
            parameters.Add("out_ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("Out_ErrorMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
            parameters.Add("out_OrgId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_forceFullPassChage", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

            var response = await _adminUserRepository.AuthenticateUser(parameters);

            if(!string.Equals(response.UserId,authRequest.User,StringComparison.OrdinalIgnoreCase))
                throw new ApiException("User is Unauthorized",_logger);

            var userResponse = new dtoAuthResponse
            {
                UserName = response.UserName,
                UserId = response.UserId,
                Corporation = response.Corporation,
                CorporationAddress = response.CorporationAddress,
                ReceiptOfficeName = response.ReceiptOfficeName,
                ChalanOfficeName = response.ChalanOfficeName,
                PrabhagName = response.PrabhagName,
                PrabhagId = response.PrabhagId,
                DesigId = response.DesigId,
                UserType = response.UserType,
                CollectionCenter = response.CollectionCenter,
                MobileNo = response.MobileNo,
                OrgId = response.OrgId,
                AuthKey=Guid.NewGuid().ToString(),
                ULBId=response.UlbId
            };
            return userResponse;

        }



    }
}
