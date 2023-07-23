using app.advertise.DataAccess.Admin;
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
    public class HoardingtypeMasterService: IHoardingtypeMasterService
    {
        private readonly IHoardingtypeMasterRepository _hoardingtypeMasterRepository;
        private readonly UserRequestHeaders _authData;
        private readonly ILogger<HoardingtypeMasterService> _logger;
        private readonly IDataProtector _dataProtector;
        private readonly IUpdateStatusRespository _updateStatusRespository;
        public HoardingtypeMasterService(IHoardingtypeMasterRepository hoardingtypeMasterRepository, UserRequestHeaders authData, IUpdateStatusRespository updateStatusRespository, ILogger<HoardingtypeMasterService> logger, DataProtectionPurpose dataProtectionPurpose, IDataProtectionProvider dataProtector)
        {
            _hoardingtypeMasterRepository = hoardingtypeMasterRepository;
            _authData = authData;
            _updateStatusRespository = updateStatusRespository;
            _logger = logger;
            _dataProtector = dataProtector.CreateProtector(dataProtectionPurpose.RecordIdRouteValue);
        }


        public async Task Insert(dtoHoardingtypeMaster dtoRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_Userid", _authData.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Hoardtypeid",0, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_Hoardypename", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Hoardypestatus", RecordStatus.A.ToString(), DbType.String, ParameterDirection.Input);
            parameters.Add("in_Ipaddress", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Source", _authData.Source, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Mode", (int)QueryExecutionMode.Insert, DbType.Int32, ParameterDirection.Input);
            
            await _hoardingtypeMasterRepository.InsertUpdate(parameters);
        }

        public async Task Update(dtoHoardingtypeMaster dtoRequest)
        {
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(dtoRequest.RecordId));
            var existingRecord = await _hoardingtypeMasterRepository.GetById(recordId) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);

            var parameters = new DynamicParameters();
            parameters.Add("in_Userid", _authData.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Hoardtypeid", existingRecord.NUM_HOARDINGTYPE_ID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_Hoardypename", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Hoardypestatus", existingRecord.VAR_HOARDINGTYPE_STATUS, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Ipaddress", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Source", _authData.Source, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Mode", (int)QueryExecutionMode.Update, DbType.Int32, ParameterDirection.Input);

            await _hoardingtypeMasterRepository.InsertUpdate(parameters);
        }

        public async Task<IEnumerable<dtoHoardingtypeMaster>> GetAll()
        {

            var result = await _hoardingtypeMasterRepository.GetAll();

            return result.Select(config => new dtoHoardingtypeMaster
            {
                Name = config.VAR_HOARDINGTYPE_NAME,
                Id = config.NUM_HOARDINGTYPE_ID,
                StatusFlag = config.VAR_HOARDINGTYPE_STATUS,
                RecordId = _dataProtector.Protect(config.NUM_HOARDINGTYPE_ID.ToString()),
            });
        }

        public async Task<dtoHoardingtypeMaster> GetById(string id)
        {
            var recordId = Convert.ToInt32(_dataProtector.Unprotect(id));
            var config = await _hoardingtypeMasterRepository.GetById(recordId);

            return new dtoHoardingtypeMaster
            {
                Name = config.VAR_HOARDINGTYPE_NAME,
                Id = config.NUM_HOARDINGTYPE_ID,
                StatusFlag = config.VAR_HOARDINGTYPE_STATUS
            };
        }

        public async Task ModifyStatusById(int id)
        {
            var existingRecord = await _hoardingtypeMasterRepository.GetById(id) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);

            existingRecord.VAR_HOARDINGTYPE_STATUS = existingRecord.VAR_HOARDINGTYPE_STATUS.ToggleStatus();
            existingRecord.DAT_HOARDINGTYPE_UPDT = DateTime.Now;
            existingRecord.VAR_HOARDINGTYPE_UPDBY = _authData.UserId;

            await _updateStatusRespository.UpdateStatus(EntityType.HoardingTypeMaster, existingRecord);
        }
    }
}
