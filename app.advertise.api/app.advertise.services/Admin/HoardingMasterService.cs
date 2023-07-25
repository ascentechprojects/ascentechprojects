using app.advertise.DataAccess.Repositories.Admin;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Admin
{
    public class HoardingMasterService : IHoardingMasterService
    {
        private readonly IHoardingMasterRepository _hoardingMasterRepository;
        private readonly UserRequestHeaders _authData;
        private readonly IUpdateStatusRespository _updateStatusRespository;
        private readonly ILogger<HoardingMasterService> _logger;
        private readonly IDataProtector _dataProtector;


        public HoardingMasterService(IHoardingMasterRepository hoardingMasterRepository, UserRequestHeaders authData, IUpdateStatusRespository updateStatusRespository, ILogger<HoardingMasterService> logger, DataProtectionPurpose dataProtectionPurpose, IDataProtectionProvider dataProtector)
        {
            _authData = authData;
            _hoardingMasterRepository = hoardingMasterRepository;
            _updateStatusRespository = updateStatusRespository;
            _logger = logger;
            _dataProtector = dataProtector.CreateProtector(dataProtectionPurpose.RecordIdRouteValue);

        }
        public async Task<IEnumerable<dtoHoardingMaster>> GetAll()
        {
            var result = await _hoardingMasterRepository.GetAll(_authData.UlbId);

            return result.Select(record => new dtoHoardingMaster
            {
                Name = record.VAR_HORDING_HOLDNAME,
                RecordId = _dataProtector.Protect(record.NUM_HORDING_ID.ToString()),
                Address = record.VAR_HORDING_HOLDADDRESS,
                HoardingType = record.VAR_HORDING_HOLDTYPE,
                DisplayTypeId = record.NUM_HORDING_DISPTYPEID,
                LocationId = record.NUM_HORDING_LOCATIONID,
                Landmark = record.VAR_HORDING_LANDMARK,
                Length = record.NUM_HORDING_LENGTH,
                TotalSQFT = record.NUM_HORDING_TOTALSQFT,
                StatusFlag = record.VAR_HORDING_ACTIVE,
                InsDt = record.DAT_HORDING_INSDT,
                UpdDt = record.DAT_HORDING_UPDT,
                Ownership = record.VAR_HORDING_OWNERSHIP,
                PrabhagName = record.VAR_PRABHAG_NAME,
                LocationName = record.VAR_LOCATION_NAME,
                DisplayTypeName = record.VAR_DISPLAYTYPE_NAME,
                HordingTypeName = record.VAR_HOARDINGTYPE_NAME,
                Id = record.NUM_HORDING_ID,
                Building=record.var_hording_buildname,
                Latitude=record.num_hording_latitude,
                Longitude=record.Num_hording_longitude
            });
        }

        public async Task<dtoHoardingMaster> GetById(string id)
        {
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(id));

            var result = await _hoardingMasterRepository.GetById(recordId, _authData.UlbId);

            return new dtoHoardingMaster
            {
                Name = result.VAR_HORDING_HOLDNAME,
                Id = result.NUM_HORDING_ID,
                Address = result.VAR_HORDING_HOLDADDRESS,
                HoardingType = result.VAR_HORDING_HOLDTYPE,
                DisplayTypeId = result.NUM_HORDING_DISPTYPEID,
                PrabhagId = result.NUM_HORDING_PRABHAGID,
                LocationId = result.NUM_HORDING_LOCATIONID,
                Landmark = result.VAR_HORDING_LANDMARK,
                Length = result.NUM_HORDING_LENGTH,
                Width = result.NUM_HORDING_WIDTH,
                TotalSQFT = result.NUM_HORDING_TOTALSQFT,
                StatusFlag = result.VAR_HORDING_ACTIVE,
                Ownership = result.VAR_HORDING_OWNERSHIP,
                RecordId = _dataProtector.Protect(result.NUM_HORDING_ID.ToString()),
            };
        }

        public async Task<dtoHoardingMaster> GetById(int recordId)
        {

            var result = await _hoardingMasterRepository.GetById(recordId, _authData.UlbId);

            return new dtoHoardingMaster
            {
                Name = result.VAR_HORDING_HOLDNAME,
                Address = result.VAR_HORDING_HOLDADDRESS,
                HoardingType = result.VAR_HORDING_HOLDTYPE,
                DisplayTypeId = result.NUM_HORDING_DISPTYPEID,
                PrabhagId = result.NUM_HORDING_PRABHAGID,
                LocationId = result.NUM_HORDING_LOCATIONID,
                Landmark = result.VAR_HORDING_LANDMARK,
                Length = result.NUM_HORDING_LENGTH,
                Width = result.NUM_HORDING_WIDTH,
                TotalSQFT = result.NUM_HORDING_TOTALSQFT,
                StatusFlag = result.VAR_HORDING_ACTIVE,
                Ownership = result.VAR_HORDING_OWNERSHIP,
                DisplayTypeName=result.VAR_DISPLAYTYPE_NAME,
                HordingTypeName=result.VAR_HOARDINGTYPE_NAME
            };
        }

        public async Task Insert(dtoHoardingMaster dto)
        {
            var parameters = new DynamicParameters();

            parameters.Add("in_UserId", _authData.UserId);
            parameters.Add("in_ULBID", _authData.UlbId);
            parameters.Add("in_prabhagid", dto.PrabhagId);
            parameters.Add("in_locationid", dto.LocationId);
            parameters.Add("in_holdid", 0);
            parameters.Add("in_holdtype", dto.HoardingType);
            parameters.Add("in_holdname", dto.Name);
            parameters.Add("in_buildname", dto.Building);
            parameters.Add("in_holdaddress", dto.Address);
            parameters.Add("in_landmark", dto.Landmark);
            parameters.Add("in_ownership", dto.Ownership);
            parameters.Add("in_disptypeid", dto.DisplayTypeId);
            parameters.Add("in_latitude", dto.Latitude);
            parameters.Add("in_longitude", dto.Longitude);
            parameters.Add("in_length", dto.Length);
            parameters.Add("in_width", dto.Width);
            parameters.Add("in_totalsqft", dto.TotalSQFT);
            parameters.Add("in_active", RecordStatus.A.ToString());
            parameters.Add("in_ipaddress", _authData.IpAddress);
            parameters.Add("in_source", _authData.Source);
            parameters.Add("in_mode", (int)QueryExecutionMode.Insert);

            await _hoardingMasterRepository.InsertUpdate(parameters);
        }

        public async Task Update(dtoHoardingMaster dto)
        {
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(dto.RecordId));
            var existingRecord = await _hoardingMasterRepository.GetById(recordId, _authData.UlbId) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);

            var parameters = new DynamicParameters();
            parameters.Add("in_UserId", _authData.UserId);
            parameters.Add("in_ULBID", existingRecord.NUM_HORDING_ULBID);
            parameters.Add("in_prabhagid", dto.PrabhagId);
            parameters.Add("in_locationid", dto.LocationId);
            parameters.Add("in_holdid", existingRecord.NUM_HORDING_ID);
            parameters.Add("in_holdtype", dto.HoardingType);
            parameters.Add("in_holdname", dto.Name);
            parameters.Add("in_buildname", dto.Building);
            parameters.Add("in_holdaddress", dto.Address);
            parameters.Add("in_landmark", dto.Landmark);
            parameters.Add("in_ownership", dto.Ownership);
            parameters.Add("in_disptypeid", dto.DisplayTypeId);
            parameters.Add("in_latitude", dto.Latitude);
            parameters.Add("in_longitude", dto.Longitude);
            parameters.Add("in_length", dto.Length);
            parameters.Add("in_width", dto.Width);
            parameters.Add("in_totalsqft", dto.TotalSQFT);
            parameters.Add("in_active", existingRecord.VAR_HORDING_ACTIVE);
            parameters.Add("in_ipaddress", _authData.IpAddress);
            parameters.Add("in_source", _authData.Source);
            parameters.Add("in_mode", (int)QueryExecutionMode.Update);

            await _hoardingMasterRepository.InsertUpdate(parameters);
        }

        public async Task ModifyStatusById(int id)
        {
            var existingRecord = await _hoardingMasterRepository.GetById(id, _authData.UlbId) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);

            existingRecord.VAR_HORDING_ACTIVE = existingRecord.VAR_HORDING_ACTIVE.ToggleStatus();
            existingRecord.DAT_HORDING_UPDT = DateTime.Now;
            existingRecord.VAR_HORDING_UPDBY = _authData.UserId;

            await _updateStatusRespository.UpdateStatus(EntityType.HoardingMaster, existingRecord);
        }
    }
}
