using app.advertise.DataAccess;
using app.advertise.dtos;
using app.advertise.libraries;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Admin
{
    public class ApplicationMasterService : IApplicationMasterService
    {
        private readonly IApplicationMasterRespository _repository;
        private readonly ILogger<HoardingMasterService> _logger;
        public ApplicationMasterService(IApplicationMasterRespository repository, ILogger<HoardingMasterService> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IEnumerable<dtoApplicationAuthResult>> AuthSerach(dtoApplicationAuthRequest dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_location_id", dto.LocationId);
            parameters.Add("p_prabhag_id", dto.PrabhagId);
            var result = await _repository.AuthSearch(parameters);

            return result.Select(record => new dtoApplicationAuthResult
            {
                ApplicationNo = record.VAR_APPLI_APPLINO,
                AppliAppName = record.VAR_APPLI_APPLINAME,
                AppliId = record.NUM_APPLI_ID,
                AppliLicenseNo = record.VAR_APPLI_LICENO,
                AppliLicenseOutNo = record.VAR_APPLI_LICEOUTNO,
                AppliAddress = record.VAR_APPLI_ADDRESS,
                AppliEmail = record.VAR_APPLI_EMAIL,
                AppliMobileNo = record.NUM_APPLI_MOBILENO,
                AppliFromDate = record.DAT_APPLI_FROMDT.ToString(AppConstants.Date_Dafault_Format),
                AppliUpToDate = record.DAT_APPLI_UPTODT.ToString(AppConstants.Date_Dafault_Format),
                AppliInsBy = record.VAR_APPLI_INSBY,
                AppliInsDt = record.DAT_APPLI_INSDT,
                HordingHoldName = record.VAR_HORDING_HOLDNAME,
                HordingHoldAddress = record.VAR_HORDING_HOLDADDRESS,
                HordingLength = record.NUM_HORDING_LENGTH,
                HordingWidth = record.NUM_HORDING_WIDTH,
                HordingTotalSqFt = record.NUM_HORDING_TOTALSQFT,
                DisplayTypeName = record.VAR_DISPLAYTYPE_NAME,
                HordingOwnership = StaticHelpers.HoardingOwnerships().TryGetValue(record.VAR_HORDING_OWNERSHIP, out var value) && value != null && !string.IsNullOrEmpty(value) ? value : string.Empty,

            });
        }

        public async Task<dtoApplicationAuthResult> AppliDetailsbyId(int Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_appli_id", Id);
            var record = await _repository.ApplicationById(parameters);

            return new dtoApplicationAuthResult()
            {
                ApplicationNo = record.VAR_APPLI_APPLINO,
                AppliAppName = record.VAR_APPLI_APPLINAME,
                AppliId = record.NUM_APPLI_ID,
                AppliLicenseNo = record.VAR_APPLI_LICENO,
                AppliLicenseOutNo = record.VAR_APPLI_LICEOUTNO,
                AppliAddress = record.VAR_APPLI_ADDRESS,
                AppliEmail = record.VAR_APPLI_EMAIL,
                AppliMobileNo = record.NUM_APPLI_MOBILENO,
                AppliFromDate = record.DAT_APPLI_FROMDT.ToString(AppConstants.Date_Dafault_Format),
                AppliUpToDate = record.DAT_APPLI_UPTODT.ToString(AppConstants.Date_Dafault_Format),
                AppliInsBy = record.VAR_APPLI_INSBY,
                AppliInsDt = record.DAT_APPLI_INSDT,
                HordingHoldName = record.VAR_HORDING_HOLDNAME,
                HordingHoldAddress = record.VAR_HORDING_HOLDADDRESS,
                HordingOwnership = record.VAR_HORDING_OWNERSHIP,//StaticHelpers.HoardingOwnerships().TryGetValue(record.VAR_HORDING_OWNERSHIP, out var value) && value != null ? value.ToString() : string.Empty,
                HordingLength = record.NUM_HORDING_LENGTH,
                HordingWidth = record.NUM_HORDING_WIDTH,
                HordingTotalSqFt = record.NUM_HORDING_TOTALSQFT,
                DisplayTypeName = record.VAR_DISPLAYTYPE_NAME
            };
        }
    }
}
