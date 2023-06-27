using app.advertise.DataAccess;
using app.advertise.DataAccess.Entities;
using app.advertise.dtos;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Admin
{
    public class DisplayTypeMasterService : IDisplayTypeMasterService
    {
        private readonly ILogger<DisplayTypeMasterService> _logger;
        private readonly IDisplayTypeMasterRepository _displayTypeMasterRepository;
        private readonly UseroAuthClaims _authClaims;
        public DisplayTypeMasterService(ILogger<DisplayTypeMasterService> logger, IDisplayTypeMasterRepository displayTypeMasterRepository)
        {
            _authClaims=new UseroAuthClaims();
            _logger = logger;
            _displayTypeMasterRepository = displayTypeMasterRepository;
        }

        public async Task InsertUpdate(dtoDisplayTypeMaster dtoRequest, QueryExecutionMode mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_Userid", _authClaims.Userid, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypeid", dtoRequest.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_disptypename", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypestatus", dtoRequest.StatusFlag, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Ipaddress", _authClaims.IPAddress??dtoRequest.IPAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Source", _authClaims.Source, DbType.String, ParameterDirection.Input);
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
                VAR_DISPLAYTYPE_UPDBY = _authClaims.Userid
            };
            await _displayTypeMasterRepository.ModifyStatusById(parameters);
        }
    }
}
