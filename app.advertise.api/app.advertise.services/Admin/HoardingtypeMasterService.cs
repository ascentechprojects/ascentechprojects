using app.advertise.DataAccess;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Admin
{
    public class HoardingtypeMasterService: IHoardingtypeMasterService
    {
        private readonly ILogger<HoardingtypeMasterService> _logger;
        private readonly IHoardingtypeMasterRepository _hoardingtypeMasterRepository;
        private readonly UserRequestHeaders _authData;
        public HoardingtypeMasterService(ILogger<HoardingtypeMasterService> logger, IHoardingtypeMasterRepository hoardingtypeMasterRepository, UserRequestHeaders authData)
        {
            _logger = logger;
            _hoardingtypeMasterRepository = hoardingtypeMasterRepository;
            _authData = authData;
        }

        public async Task InsertUpdate(dtoHoardingtypeMaster dtoRequest, QueryExecutionMode mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_Userid", _authData.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Hoardtypeid", dtoRequest.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_Hoardypename", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Hoardypestatus", dtoRequest.HoardTypestatus, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Ipaddress", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Source", _authData.Source, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Mode", mode, DbType.Int32, ParameterDirection.Input);
            
            await _hoardingtypeMasterRepository.InsertUpdate(parameters);
        }

        public async Task<IEnumerable<dtoHoardingtypeMaster>> GetAll()
        {

            var result = await _hoardingtypeMasterRepository.GetAll();

            return result.Select(config => new dtoHoardingtypeMaster
            {
                Name = config.VAR_HOARDINGTYPE_NAME,
                Id = config.NUM_HOARDINGTYPE_ID,
                HoardTypestatus = config.VAR_HOARDINGTYPE_STATUS,
                InsDt=config.DAT_HOARDINGTYPE_INSDT,
                UpdDt=config.DAT_HOARDINGTYPE_UPDT
            });
        }

        public async Task<dtoHoardingtypeMaster> GetById(int id)
        {

            var config = await _hoardingtypeMasterRepository.GetById(id);

            return new dtoHoardingtypeMaster
            {
                Name = config.VAR_HOARDINGTYPE_NAME,
                Id = config.NUM_HOARDINGTYPE_ID,
                HoardTypestatus = config.VAR_HOARDINGTYPE_STATUS
            };
        }
    }
}
