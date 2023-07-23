using app.advertise.DataAccess.Repositories.Admin;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
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
            var result = new dtoDashboard();

            var records = await _adminDashboardRepository.DashboardData(_authData.UlbId);

            if (!records.FlagStatus.All(record => StaticHelpers.RemarkStatus().ContainsKey(record.VAR_APPLI_APPROVFLAG)) || !records.PrabhagOverview.All(record => StaticHelpers.RemarkStatus().ContainsKey(record.VAR_APPLI_APPROVFLAG)))
                throw new ApiException("Invalid Status key", logger: _logger);

            var statusDictionary = records.FlagStatus.ToDictionary(x => x.VAR_APPLI_APPROVFLAG, x => x.APPROVFLAG_StatusCount);

            result.ApplicationStatus = new dtoDashboardStatus()
            {
                StatusFlag_Pending = GetStatusCount(statusDictionary, "P"),
                StatusFlag_Approved = GetStatusCount(statusDictionary, "A"),
                StatusFlag_Closed = GetStatusCount(statusDictionary, "C"),
                StatusFlag_Rejected = GetStatusCount(statusDictionary, "R"),
            };
            //To Do: Pivod This and one more properties TotalApplication
            result.PrabhagOverview = records.PrabhagOverview.Select(x => new dtoDashboardPrabhagOverview()
            {
                PrabhagName = x.VAR_PRABHAG_NAME,
                PrabhagId = x.NUM_APPLI_PRABHAGID,
                AppliStatusFlag = StaticHelpers.RemarkStatus()[x.VAR_APPLI_APPROVFLAG],
                StatusFlagTotal = x.APPROVFLAG_StatusCount
            });

            return result;
        }

        private static int GetStatusCount(Dictionary<string, int> statusDictionary, string statusFlag) => statusDictionary.TryGetValue(statusFlag, out int statusCount) ? statusCount : 0;
    }
}
