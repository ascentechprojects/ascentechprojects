using app.advertise.DataAccess;
using app.advertise.DataAccess.Entities;
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
    public class DisplayTypeMasterService : IDisplayTypeMasterService
    {
        private readonly IDisplayTypeMasterRepository _displayTypeMasterRepository;
        private readonly UserRequestHeaders _authData;
        private readonly IUpdateStatusRespository _updateStatusRespository;
        private readonly ILogger<HoardingMasterService> _logger;
        private readonly IDataProtector _dataProtector;
        public DisplayTypeMasterService(IDisplayTypeMasterRepository displayTypeMasterRepository, UserRequestHeaders authData, IUpdateStatusRespository updateStatusRespository, ILogger<HoardingMasterService> logger, DataProtectionPurpose dataProtectionPurpose, IDataProtectionProvider dataProtector)
        {
            _authData = authData;
            _displayTypeMasterRepository = displayTypeMasterRepository;
            _updateStatusRespository = updateStatusRespository;
            _logger = logger;
            _dataProtector = dataProtector.CreateProtector(dataProtectionPurpose.RecordIdRouteValue);
        }

        public async Task Insert(dtoDisplayTypeMaster dtoRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_Userid", _authData.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypeid", 0, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_disptypename", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypestatus", RecordStatus.A.ToString(), DbType.String, ParameterDirection.Input);
            parameters.Add("in_Ipaddress", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Source", _authData.Source, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Mode", (int)QueryExecutionMode.Insert, DbType.Int32, ParameterDirection.Input);

            await _displayTypeMasterRepository.InsertUpdate(parameters);
        }

        public async Task Update(dtoDisplayTypeMaster dtoRequest)
        {
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(dtoRequest.RecordId));
            var existingRecord = await _displayTypeMasterRepository.GetById(recordId) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);


            var parameters = new DynamicParameters();
            parameters.Add("in_Userid", _authData.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypeid", existingRecord.NUM_DISPLAYTYPE_ID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_disptypename", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypestatus", existingRecord.VAR_DISPLAYTYPE_STATUS, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Ipaddress", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Source", _authData.Source, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Mode", (int)QueryExecutionMode.Update, DbType.Int32, ParameterDirection.Input);

            await _displayTypeMasterRepository.InsertUpdate(parameters);
        }

        public async Task<IEnumerable<dtoDisplayTypeMaster>> GetAll()
        {

            var records = await _displayTypeMasterRepository.GetAll();

            return records.Select(record => new dtoDisplayTypeMaster
            {
                Name = record.VAR_DISPLAYTYPE_NAME,
                Id = record.NUM_DISPLAYTYPE_ID,
                StatusFlag = record.VAR_DISPLAYTYPE_STATUS,
                InsDt = record.DAT_DISPLAYTYPE_INSDT,
                UpdDt = record.DAT_DISPLAYTYPE_UPDT,
                RecordId = _dataProtector.Protect(record.NUM_DISPLAYTYPE_ID.ToString()),
            });
        }

        public async Task<dtoDisplayTypeMaster> GetById(string id)
        {
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(id));
            var config = await _displayTypeMasterRepository.GetById(recordId);

            return new dtoDisplayTypeMaster
            {
                Name = config.VAR_DISPLAYTYPE_NAME,
                Id = config.NUM_DISPLAYTYPE_ID,
                StatusFlag = config.VAR_DISPLAYTYPE_STATUS
            };
        }

        public async Task ModifyStatusById(int id)
        {
            var existingRecord = await _displayTypeMasterRepository.GetById(id) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);

            existingRecord.VAR_DISPLAYTYPE_STATUS = existingRecord.VAR_DISPLAYTYPE_STATUS.ToggleStatus();
            existingRecord.DAT_DISPLAYTYPE_UPDT = DateTime.Now;
            existingRecord.VAR_DISPLAYTYPE_UPDBY = _authData.UserId;

            await _updateStatusRespository.UpdateStatus(EntityType.DisplayTypeMaster, existingRecord);
        }

        public async Task<IEnumerable<dtoDisplayTypeMaster>> ActiveDisplayTypes()
        {

            var result = await _displayTypeMasterRepository.ActiveDisplayTypes();

            return result.Select(config => new dtoDisplayTypeMaster
            {
                Name = config.VAR_DISPLAYTYPE_NAME,
                Id = config.NUM_DISPLAYTYPE_ID,
                StatusFlag = config.VAR_DISPLAYTYPE_STATUS,
            });
        }

        public async Task<IEnumerable<dtoDisplayTypeMaster>> DisplayTypesExistsInConfig(int displayConfigUlbId)
        {

            var result = await _displayTypeMasterRepository.DisplayTypesExistsInConfig(displayConfigUlbId);

            return result.Select(config => new dtoDisplayTypeMaster
            {
                Name = config.VAR_DISPLAYTYPE_NAME,
                Id = config.NUM_DISPLAYTYPE_ID,
                ConfigUlbId = config.NUM_DISPLAYCONFIG_ULBID,
                IsExistsInConfig = config.ExistsInConfig == 1 ? true : false,
                DisplayConfigId = config.NUM_DISPLAYCONFIG_ID
            });
        }

        public async Task AddUpdateDisplayConfig(IEnumerable<dtoDisplayTypeMaster> dto)
        {

            foreach (var item in dto)
            {
                var parameters = new DynamicParameters();
                parameters.Add("In_UserId", _authData.UserId);
                parameters.Add("In_OrgId", item.ULBId);
                parameters.Add("In_DisplayTypeStr", item.Id);
                parameters.Add("In_Mode", item.DisplayConfigId > 0 ? (int)QueryExecutionMode.Update : (int)QueryExecutionMode.Insert);
                parameters.Add("In_Ipaddress", _authData.IpAddress);
                parameters.Add("In_Source", _authData.Source);
                await _displayTypeMasterRepository.InsertUpdateConfig(parameters);

            }

        }

    }
}
