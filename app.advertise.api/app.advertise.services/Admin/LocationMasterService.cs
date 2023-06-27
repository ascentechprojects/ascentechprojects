using app.advertise.DataAccess;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Admin
{
    public class LocationMasterService : ILocationMasterService
    {
        private readonly ILogger<LocationMasterService> _logger;
        private readonly ILocationMasterRepository _locationMasterRepository;
        private readonly UseroAuthClaims _authClaims;
        public LocationMasterService(ILogger<LocationMasterService> logger, ILocationMasterRepository locationMasterRepository)
        {
            _logger = logger;
            _locationMasterRepository = locationMasterRepository;
        }

        public async Task InsertUpdate(dtoLocationMaster dtoRequest, QueryExecutionMode mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_UserId", _authClaims.Userid, DbType.String, ParameterDirection.Input);
            parameters.Add("in_UlbId", dtoRequest.ULBId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_locatId", dtoRequest.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_PrabhagId", dtoRequest.PrabhagId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_locatname", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_area", dtoRequest.Area, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_pincode", dtoRequest.PinCode, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_Active", dtoRequest.LocationActive, DbType.String, ParameterDirection.Input);
            parameters.Add("in_Latitude", dtoRequest.LATITUDE, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("in_Longitude", dtoRequest.LONGITUDE, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("in_Mode", mode, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_ipaddress", _authClaims.IPAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_source", _authClaims.Source, DbType.String, ParameterDirection.Input);

            await _locationMasterRepository.InsertUpdate(parameters);
        }

        public async Task<IEnumerable<dtoLocationMaster>> GetAll()
        {

            var result = await _locationMasterRepository.GetAll();

            return result.Select(config => new dtoLocationMaster
            {
                Name = config.VAR_LOCATION_NAME,
                Id = config.NUM_LOCATION_ID,
                LocationActive = config.VAR_LOCATION_ACTIVE,
                PinCode=config.NUM_LOCATION_PINCODE,
                Area=config.NUM_LOCATION_AREA,
            });
        }

        public async Task<dtoLocationMaster> GetById(int id)
        {

            var config = await _locationMasterRepository.GetById(id);

            return new dtoLocationMaster
            {
                Name = config.VAR_LOCATION_NAME,
                Id = config.NUM_LOCATION_ID,
                LocationActive = config.VAR_LOCATION_ACTIVE,
                PinCode = config.NUM_LOCATION_PINCODE,
                Area = config.NUM_LOCATION_AREA,
            };
        }
    }
}
