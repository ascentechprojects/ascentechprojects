using app.advertise.DataAccess.Repositories.Admin;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.services.Admin.Interfaces;
using app.advertise.services.Interfaces;
using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Admin
{
    public class ApplicationMasterService : IApplicationMasterService
    {
        private readonly IApplicationMasterRespository _repository;
        private readonly UserRequestHeaders _authData;
        private readonly IDataProtector _dataProtector;
        private readonly ILogger<ApplicationMasterService> _logger;
        private readonly IFileService _fileService;
        public ApplicationMasterService(IApplicationMasterRespository repository, UserRequestHeaders authData, DataProtectionPurpose dataProtectionPurpose, IDataProtectionProvider dataProtector, ILogger<ApplicationMasterService> logger, IFileService fileService)
        {
            _repository = repository;
            _authData = authData;
            _dataProtector = dataProtector.CreateProtector(dataProtectionPurpose.RecordIdRouteValue);
            _logger = logger;
            _fileService = fileService;
        }

        public async Task<IEnumerable<dtoApplicationAuthResult>> AuthSerach(dtoApplicationAuthRequest dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_location_id", dto.LocationId);
            parameters.Add("p_prabhag_id", dto.PrabhagId);
            parameters.Add("p_ulb_id", _authData.UlbId);
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
                AppliInsDt = record.DAT_APPLI_INSDT.ToString(AppConstants.Date_Dafault_Format),
                HordingHoldName = record.VAR_HORDING_HOLDNAME,
                HordingHoldAddress = record.VAR_HORDING_HOLDADDRESS,
                HordingLength = record.NUM_HORDING_LENGTH,
                HordingWidth = record.NUM_HORDING_WIDTH,
                HordingTotalSqFt = record.NUM_HORDING_TOTALSQFT,
                DisplayTypeName = record.VAR_DISPLAYTYPE_NAME,
                RemarkFlag = StaticHelpers.RemarkStatus().FirstOrDefault(x => x.Key.Equals(record.VAR_APPLI_APPROVFLAG)).Value,
                HordingOwnership = StaticHelpers.HoardingOwnerships().TryGetValue(record.VAR_HORDING_OWNERSHIP, out var value) && value != null && !string.IsNullOrEmpty(value) ? value : string.Empty,
                PrabhagName = record.VAR_PRABHAG_NAME,
                LocationName = record.VAR_LOCATION_NAME,
                RecordId = _dataProtector.Protect(record.NUM_APPLI_ID.ToString())
            });
        }

        public async Task<dtoApplicationAuthResult> AppliDetailsbyId(string id)
        {
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(id));

            var parameters = new DynamicParameters();
            parameters.Add("p_appli_id", recordId);
            parameters.Add("p_ulb_id", _authData.UlbId);

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
                AppliInsDt = record.DAT_APPLI_INSDT.ToString(AppConstants.Date_Dafault_Format),
                HordingHoldName = record.VAR_HORDING_HOLDNAME,
                HordingHoldAddress = record.VAR_HORDING_HOLDADDRESS,
                HordingOwnership = StaticHelpers.HoardingOwnerships().TryGetValue(record.VAR_HORDING_OWNERSHIP, out var value) && value != null && !string.IsNullOrEmpty(value) ? value : string.Empty,
                HordingLength = record.NUM_HORDING_LENGTH,
                HordingWidth = record.NUM_HORDING_WIDTH,
                HordingTotalSqFt = record.NUM_HORDING_TOTALSQFT,
                DisplayTypeName = record.VAR_DISPLAYTYPE_NAME,
                PrabhagName = record.VAR_PRABHAG_NAME,
                LocationName = record.VAR_LOCATION_NAME,
                RemarkFlag = StaticHelpers.RemarkStatus().FirstOrDefault(x => x.Key.Equals(record.VAR_APPLI_APPROVFLAG)).Value,
                Quantity = record.NUM_APPLI_QTY,
                Remark = record.VAR_APPLI_APPROVREMARK,
                AppImage= _fileService.ByteToBase64(record.BLO_APPLIDOC_IMAGE)
            };
        }

        public async Task UpdateStatusFlag(dtoApplicationAuthRequest dto)
        {
            var statusKey = StaticHelpers.RemarkStatus().FirstOrDefault(x => x.Value.Equals(dto.StatusFlag)).Key;
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(dto.RecordId));
            var appClose = await _repository.ApplicationCloseByAppId(new() { NUM_APPLICLOSE_ULBID = _authData.UlbId, NUM_APPLICLOSE_APPID = recordId });


            var parameters = new DynamicParameters();
            parameters.Add("in_ulbID", _authData.UlbId);
            parameters.Add("in_userid", _authData.UserId);
            parameters.Add("IN_AppCloseID", appClose.NUM_APPLICLOSE_ID);
            parameters.Add("IN_AppliID", recordId);
            parameters.Add("in_remark", dto.Remark);
            parameters.Add("in_AppStatus", statusKey);
            parameters.Add("in_ipaddress", _authData.IpAddress);
            parameters.Add("in_source", _authData.Source.ToString());

            await _repository.UpdateAppliStatus(parameters);
        }

        public async Task<IEnumerable<dtoApplicationAuthResult>> DeauthSearch(dtoApplicationAuthRequest dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_location_id", dto.LocationId);
            parameters.Add("p_prabhag_id", dto.PrabhagId);
            parameters.Add("p_ulb_id", _authData.UlbId);
            var result = await _repository.DeauthSearch(parameters);

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
                AppliInsDt = record.DAT_APPLI_INSDT.ToString(AppConstants.Date_Dafault_Format),
                HordingHoldName = record.VAR_HORDING_HOLDNAME,
                HordingHoldAddress = record.VAR_HORDING_HOLDADDRESS,
                HordingLength = record.NUM_HORDING_LENGTH,
                HordingWidth = record.NUM_HORDING_WIDTH,
                HordingTotalSqFt = record.NUM_HORDING_TOTALSQFT,
                DisplayTypeName = record.VAR_DISPLAYTYPE_NAME,
                RemarkFlag = StaticHelpers.RemarkStatus().FirstOrDefault(x => x.Key.Equals(record.VAR_APPLI_APPROVFLAG)).Value,
                HordingOwnership = StaticHelpers.HoardingOwnerships().TryGetValue(record.VAR_HORDING_OWNERSHIP, out var value) && value != null && !string.IsNullOrEmpty(value) ? value : string.Empty,
                PrabhagName = record.VAR_PRABHAG_NAME,
                LocationName = record.VAR_LOCATION_NAME,
                RecordId = _dataProtector.Protect(record.NUM_APPLI_ID.ToString())
            });
        }

        public async Task<string> DeauthStatus(dtoApplicationAuthRequest dto)
        {
            var statusKey = StaticHelpers.RemarkStatus().FirstOrDefault(x => x.Value.Equals(dto.StatusFlag)).Key;
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(dto.RecordId));

            var appParameters = new DynamicParameters();
            appParameters.Add("p_appli_id", recordId);
            appParameters.Add("p_ulb_id", _authData.UlbId);

            var application = await _repository.ApplicationById(appParameters) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);
            var appClose = await _repository.ApplicationCloseByAppId(new() { NUM_APPLICLOSE_ULBID = application.NUM_APPLI_ULBID, NUM_APPLICLOSE_APPID = application.NUM_APPLI_ID });

            var appCloseParameters = new DynamicParameters();
            appCloseParameters.Add("in_ulbID", _authData.UlbId);
            appCloseParameters.Add("in_userid", _authData.UserId);
            appCloseParameters.Add("IN_AppCloseID", appClose.NUM_APPLICLOSE_ID);
            appCloseParameters.Add("in_Holding", application.NUM_APPLI_HORDINGID);
            appCloseParameters.Add("in_remark", dto.Remark);
            appCloseParameters.Add("in_STR", application.VAR_APPLI_APPLINO);
            appCloseParameters.Add("in_ipaddress", _authData.IpAddress);
            appCloseParameters.Add("in_source", _authData.Source.ToString());

            var result= await _repository.DeauthAppliStatus(appCloseParameters);
            return result.VAR_APPLICLOSE_ID;
        }
    }
}
