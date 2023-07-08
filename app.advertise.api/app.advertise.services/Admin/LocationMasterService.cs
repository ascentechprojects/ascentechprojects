using app.advertise.DataAccess;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.services.Admin
{
    public class LocationMasterService : ILocationMasterService
    {
        private readonly ILocationMasterRepository _locationMasterRepository;
        private readonly UserRequestHeaders _authData;
        private readonly IUpdateStatusRespository _updateStatusRespository;
        private readonly ILogger<LocationMasterService> _logger;
        public LocationMasterService(UserRequestHeaders authData, ILocationMasterRepository locationMasterRepository, IUpdateStatusRespository updateStatusRespository, ILogger<LocationMasterService> logger)
        {
           _authData=authData;
            _locationMasterRepository = locationMasterRepository;
            _logger= logger;
            _updateStatusRespository=updateStatusRespository;

        }

        public async Task InsertUpdate(dtoLocationMaster dto, QueryExecutionMode mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_UserId", _authData.UserId);
            parameters.Add("in_UlbId", _authData.UlbId);
            parameters.Add("in_locatId", dto.Id);
            parameters.Add("in_PrabhagId", dto.PrabhagId);
            parameters.Add("in_locatname", dto.Name);
            parameters.Add("in_area", dto.Area);
            parameters.Add("in_pincode", dto.PinCode);
            parameters.Add("in_Active", RecordStatus.A.ToString());
            parameters.Add("in_Latitude", dto.LATITUDE);
            parameters.Add("in_Longitude", dto.LONGITUDE);
            parameters.Add("in_Mode", (int)mode);
            parameters.Add("in_ipaddress", _authData.IpAddress);
            parameters.Add("in_source", _authData.Source);

            await _locationMasterRepository.InsertUpdate(parameters);
        }

        public async Task<IEnumerable<dtoLocationMaster>> GetAll()
        {

            var result = await _locationMasterRepository.GetAll(_authData.UlbId);

            return result.Select(config => new dtoLocationMaster
            {
                Name = config.VAR_LOCATION_NAME,
                Id = config.NUM_LOCATION_ID,
                StatusFlag = config.VAR_LOCATION_ACTIVE,
                PinCode=config.NUM_LOCATION_PINCODE,
                Area=config.NUM_LOCATION_AREA,
                PrabhagId=config.NUM_LOCATION_PRABHAGID,
                PrabhagName=config.VAR_PRABHAG_NAME
            });
        }

        public async Task<dtoLocationMaster> GetById(int id)
        {

            var config = await _locationMasterRepository.GetById(id);

            return new dtoLocationMaster
            {
                Name = config.VAR_LOCATION_NAME,
                Id = config.NUM_LOCATION_ID,
                StatusFlag = config.VAR_LOCATION_ACTIVE,
                PinCode = config.NUM_LOCATION_PINCODE,
                Area = config.NUM_LOCATION_AREA,
                PrabhagId=config.NUM_LOCATION_PRABHAGID,
                LONGITUDE=config.NUM_LOCATION_LONGITUDE,
                LATITUDE=config.NUM_LOCATION_LATITUDE
            };
        }

        public async Task ModifyStatusById(int id)
        {
            var existingRecord = await _locationMasterRepository.GetById(id) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);

            existingRecord.VAR_LOCATION_ACTIVE = existingRecord.VAR_LOCATION_ACTIVE.ToggleStatus();
            existingRecord.DAT_LOCATION_UPDT = DateTime.Now;
            existingRecord.VAR_LOCATION_UPDBY = _authData.UserId;

            await _updateStatusRespository.UpdateStatus(EntityType.LocationMaster, existingRecord);
        }
    }
}
