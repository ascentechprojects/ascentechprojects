using app.advertise.DataAccess;
using app.advertise.DataAccess.Entities;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using System.Data;

namespace app.advertise.services.Admin
{
    public class DisplayTypeMasterService : IDisplayTypeMasterService
    {
        private readonly IDisplayTypeMasterRepository _displayTypeMasterRepository;
        private readonly UserRequestHeaders _authData;
        public DisplayTypeMasterService(IDisplayTypeMasterRepository displayTypeMasterRepository, UserRequestHeaders authData)
        {
            _authData=authData;
            _displayTypeMasterRepository = displayTypeMasterRepository;
        }

        public async Task InsertUpdate(dtoDisplayTypeMaster dtoRequest, QueryExecutionMode mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_Userid", _authData.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypeid", dtoRequest.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_disptypename", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypestatus", dtoRequest.StatusFlag, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Ipaddress", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Source", _authData.Source, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Mode", mode, DbType.Int32, ParameterDirection.Input);

            await _displayTypeMasterRepository.InsertUpdate(parameters);
        }

        public async Task<IEnumerable<dtoDisplayTypeMaster>> GetAll()
        {

            var result = await _displayTypeMasterRepository.GetAll();

            return result.Select(config => new dtoDisplayTypeMaster
            {
                Name = config.VAR_DISPLAYTYPE_NAME,
                Id = config.NUM_DISPLAYTYPE_ID,
                StatusFlag = config.VAR_DISPLAYTYPE_STATUS,
                InsDt = config.DAT_DISPLAYTYPE_INSDT,
                UpdDt = config.DAT_DISPLAYTYPE_UPDT
            });
        }

        public async Task<dtoDisplayTypeMaster> GetById(int id)
        {

            var config = await _displayTypeMasterRepository.GetById(id);

            return new dtoDisplayTypeMaster
            {
                Name = config.VAR_DISPLAYTYPE_NAME,
                Id = config.NUM_DISPLAYTYPE_ID,
                StatusFlag = config.VAR_DISPLAYTYPE_STATUS
            };
        }

        public async Task ModifyStatusById(int id, string status)
        {
            var parameters = new DisplayTypeMaster()
            {
                NUM_DISPLAYTYPE_ID = id,
                VAR_DISPLAYTYPE_STATUS = status,
                DAT_DISPLAYTYPE_UPDT = DateTime.Now,
                VAR_DISPLAYTYPE_UPDBY = _authData.UserId
            };
            await _displayTypeMasterRepository.ModifyStatusById(parameters);
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

        public async Task<IEnumerable<dtoDisplayTypeMaster>>DisplayTypesExistsInConfig(int displayConfigUlbId)
        {

            var result = await _displayTypeMasterRepository.DisplayTypesExistsInConfig(displayConfigUlbId);

            return result.Select(config => new dtoDisplayTypeMaster
            {
                Name = config.VAR_DISPLAYTYPE_NAME,
                Id = config.NUM_DISPLAYTYPE_ID,
                ConfigUlbId = config.NUM_DISPLAYCONFIG_ULBID,
                IsExistsInConfig=config.ExistsInConfig==1?true:false,
                DisplayConfigId=config.NUM_DISPLAYCONFIG_ID
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
