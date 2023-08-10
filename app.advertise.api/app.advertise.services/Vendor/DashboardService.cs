using app.advertise.DataAccess.Repositories.Vendor;
using app.advertise.dtos.Vendor;
using app.advertise.libraries;
using app.advertise.services.Vendor.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Vendor
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;
        private readonly VendorRequestHeaders _authData;
        public DashboardService(IDashboardRepository repository, VendorRequestHeaders authData, ILogger<DashboardService> logger)
        {

            _authData = authData;
            _repository = repository;
        }

        public async Task<dtoDashboard> DashboardDetails()
        {
            var currentFin = StaticHelpers.CurrentFinancialYear;

            var result = new dtoDashboard() { ApplicationStatus = new dtoStatusOverview(), PrabhagOverview = Enumerable.Empty<dtoPrabhagOverview>() };
            var parameters = new DynamicParameters();
            parameters.Add("ulbId", 1, dbType:DbType.Int32);
            parameters.Add("finStart", currentFin.Start.Date, dbType: DbType.Date);
            parameters.Add("finEnd", currentFin.End.Date, dbType: DbType.Date);

            var records = await _repository.DashboardData(parameters);


            result.ApplicationStatus = new dtoStatusOverview()
            {
                Today = records.FlagStatus.Sum(x => x.Today),
                ThisMonth = records.FlagStatus.Sum(x => x.ThisMonth),
                ThisYear = records.FlagStatus.Sum(x => x.ThisYear),
                Expired = records.FlagStatus.Sum(x => x.Expired)
            };

            result.PrabhagOverview = from item in records.PrabhagOverview
                                     select new dtoPrabhagOverview()
                                     {
                                         Pending = item.Pending,
                                         PrabhaName = item.PrabhaName,
                                         Total = item.TOTALCOUNT,
                                         Sanction = item.SANCTION,
                                         Expired = item.EXPIRED,
                                     };

            return result;
        }

    }
}
