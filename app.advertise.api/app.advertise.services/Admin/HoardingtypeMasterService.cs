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
        private readonly UseroAuthClaims _authClaims;
        public HoardingtypeMasterService(ILogger<HoardingtypeMasterService> logger, IHoardingtypeMasterRepository hoardingtypeMasterRepository)
        {
            _logger = logger;
            _hoardingtypeMasterRepository = hoardingtypeMasterRepository;
        }

        public async Task InsertUpdate(dtoHoardingtypeMaster dtoRequest, QueryExecutionMode mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_Userid", _authClaims.Userid, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Hoardtypeid", dtoRequest.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_Hoardypename", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Hoardypestatus", dtoRequest.HoardTypestatus, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Ipaddress", _authClaims.IPAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Source", _authClaims.Source, DbType.String, ParameterDirection.Input);
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
                HoardTypestatus = config.VAR_HOARDINGTYPE_STATUS
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
