using app.advertise.DataAccess.Repositories.Admin;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.services.Admin.Interfaces;
using Microsoft.Extensions.Logging;

namespace app.advertise.services.Admin
{
    public class DashboardService : IDashboardService
    {
        private readonly IAdminDashboardRepository _adminDashboardRepository;
        private readonly UserRequestHeaders _authData;
        private readonly ILogger<DashboardService> _logger;
        public DashboardService(IAdminDashboardRepository adminDashboardRepository, UserRequestHeaders authData, ILogger<DashboardService> logger)
        {
            _adminDashboardRepository = adminDashboardRepository;
            _authData = authData;
            _logger = logger;
        }

        public async Task<dtoDashboard> DashboardDetails()
        {
            var result = new dtoDashboard() { ApplicationStatus = new dtoDashboardStatus(), PrabhagOverview = Enumerable.Empty<dtoDashboardPrabhagOverview>() };

            var records = await _adminDashboardRepository.DashboardData(_authData.UlbId);


            result.ApplicationStatus = new dtoDashboardStatus()
            {
                StatusFlag_Pending = records.FlagStatus.Sum(x => x.Pending),
                StatusFlag_Approved = records.FlagStatus.Sum(x => x.Approved),
                StatusFlag_Cancelled = records.FlagStatus.Sum(x => x.Cancelled),
                StatusFlag_Rejected = records.FlagStatus.Sum(x => x.Rejected)
            };

            result.PrabhagOverview = from item in records.PrabhagOverview
                                     select new dtoDashboardPrabhagOverview()
                                     {
                                         Pending = item.Pending,
                                         PrabhaName = item.VAR_PRABHAG_NAME,
                                         PrabhaId = item.NUM_APPLI_PRABHAGID,
                                         Total = item.TOTALCOUNT,
                                         Sanction = item.SANCTION,
                                         Expired = item.EXPIRED,
                                     };

            return result;
        }

    }
}
