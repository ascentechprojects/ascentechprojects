using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Admin;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess.Repositories.Admin
{
    public interface IAdminUserRepository
    {
        Task<AdminUserResponse> AuthenticateUser(DynamicParameters parameters);
    }
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<AdminUserRepository> _logger;

        public AdminUserRepository(AdvertisementDbContext context, ILogger<AdminUserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AdminUserResponse> AuthenticateUser(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("admins.aoma_login_fetch", parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_ErrorCode");
            var errorMsg = parameters.Get<string?>("Out_ErrorMsg");

            if (errorCode != 9999)
                throw new DBException(errorMsg, _logger);

            return new AdminUserResponse()
            {
                UserName = parameters.Get<string?>("Out_UserName"),
                UserId = parameters.Get<string?>("Out_userid"),
                LastLogin = parameters.Get<string?>("Out_LastLogin"),
                LastLogOut = parameters.Get<string?>("Out_LastLogOut"),
                Corporation = parameters.Get<string?>("Out_Corporation"),
                CorporationAddress = parameters.Get<string?>("Out_CorporationAddress"),
                ReceiptOfficeName = parameters.Get<string?>("Out_ReceiptOfficeName"),
                ChalanOfficeName = parameters.Get<string?>("Out_ChalanOfficeName"),
                PrabhagName = parameters.Get<string?>("Out_PrabhagName"),
                PrabhagId = parameters.Get<string?>("Out_PrabhagID"),
                DesigId = parameters.Get<string?>("Out_DesigID"),
                UserType = parameters.Get<string?>("Out_UserType"),
                CollectionCenter = parameters.Get<int?>("Out_Collectioncenter"),
                MobileNo = parameters.Get<string?>("Out_mobileno"),
                OtpValidate = parameters.Get<string?>("Out_OtpValidate"),
                OrgId = parameters.Get<int?>("out_OrgId"),
                ForceFullPassChag = parameters.Get<string?>("out_forceFullPassChage"),
                UlbId = 0
            };
        }
    }
}
