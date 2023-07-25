using app.advertise.DataAccess.Repositories.Vendor;
using app.advertise.dtos.Vendor;
using app.advertise.libraries;
using app.advertise.services.Interfaces;
using app.advertise.services.Vendor.Interfaces;
using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Vendor
{
    public class ApplicationService: IApplicationService
    {
        private readonly IApplicationRepository _repository;
        private readonly UserRequestHeaders _authData;
        private readonly IDataProtector _dataProtector;
        private readonly ILogger<ApplicationService> _logger;
        private readonly IFileService _fileService; 
        public ApplicationService(IApplicationRepository repository, UserRequestHeaders authData, DataProtectionPurpose dataProtectionPurpose, IDataProtectionProvider dataProtector, ILogger<ApplicationService> logger, IFileService fileService)
        {
            _repository = repository;
            _authData = authData;
            _dataProtector = dataProtector.CreateProtector(dataProtectionPurpose.RecordIdRouteValue);
            _logger = logger;
            _fileService = fileService;
        }

        public async Task<dtoApplicationDetails> AddApplication(dtoApplicationDetails dto, IFormFile formFile)
        {
            var parameters = new DynamicParameters();

            parameters.Add("in_ULBID", _authData.UlbId, DbType.Int32);
            parameters.Add("in_UserId", _authData.UserId, DbType.String);
            parameters.Add("in_appliid", 0, DbType.Int32);
            parameters.Add("in_applino",string.Empty, DbType.String);
            parameters.Add("in_applidt", DateTime.Now, DbType.Date);
            parameters.Add("in_liceno", dto.AppliLicenseNo, DbType.String);
            parameters.Add("in_liceoutno",dto.AppliLicenseOutNo, DbType.String);
            parameters.Add("in_appliname", dto.AppliAppName, DbType.String);
            parameters.Add("in_address", dto.AppliAddress, DbType.String);
            parameters.Add("in_email",dto.AppliEmail, DbType.String);
            parameters.Add("in_mobileno",dto.AppliMobileNo, DbType.Int32);
            parameters.Add("in_Hordingid", dto.AppliHordingId, DbType.Int32);
            parameters.Add("in_prabhagid", dto.AppliPrabhagId, DbType.Int32);
            parameters.Add("in_locationid", dto.AppliLocationId, DbType.Int32);
            parameters.Add("in_fromdt", dto.AppliFromDate, DbType.Date);
            parameters.Add("in_uptodt", dto.AppliUpToDate.AddDays(7), DbType.Date);
            parameters.Add("in_Qty", dto.Quantity, DbType.Int32);
            parameters.Add("in_approvflag",RecordStatus.A.ToString(), DbType.String);
            parameters.Add("in_approvremark",string.Empty, DbType.String);
            parameters.Add("in_ipaddress", _authData.IpAddress, DbType.String);
            parameters.Add("in_source",_authData.Source, DbType.String);
            parameters.Add("in_mode", (int)QueryExecutionMode.Insert, DbType.Int32);

            var appResult=await _repository.InserUpdateApplication(parameters);

            await _fileService.WriteFile(new dtos.dtoFormFile() {FormFile= formFile,FileName=$"{appResult.VAR_APPLI_APPLINO}.{formFile.FileName.Split('.').Last()}"});

            return new dtoApplicationDetails()
            {
                ApplicationNo = appResult.VAR_APPLI_APPLINO,
                AppliId = appResult.NUM_APPLI_ID
            };

        }

        public async Task<IEnumerable<dtoApplication>> OpenApplications()
        {
            var parameters = new DynamicParameters();
            parameters.Add("ulbId", _authData.UlbId);
            parameters.Add("userId",_authData.UserId);

            var result = await _repository.OpenApplications(parameters);

            return result.Select(record => new dtoApplication
            {
                ApplicationNo = record.VAR_APPLI_APPLINO,
                AppliAppName = record.VAR_APPLI_APPLINAME,
                RecordId = _dataProtector.Protect(record.NUM_APPLI_ID.ToString()),
                AppliLicenseNo = record.VAR_APPLI_LICENO,
                AppliDate = record.DAT_APPLI_APPLIDT.ToString(AppConstants.Date_Dafault_Format),
                HordingHoldName = record.VAR_HORDING_HOLDNAME,
                RemarkFlag = StaticHelpers.RemarkStatus().FirstOrDefault(x => x.Key.Equals(record.VAR_APPLI_APPROVFLAG)).Value,
                PrabhagName = record.VAR_PRABHAG_NAME,
                LocationName = record.VAR_LOCATION_NAME
            });
        }
    }
}
