using app.advertise.DataAccess;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.services.Admin.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;

namespace app.advertise.services.Admin
{
    public class HordingTypeConfigService : IHordingTypeConfigService
    {
        private readonly ILogger<HordingTypeConfigService> _logger;
        private readonly IHordingTypeConfigRepository _hordingTypeConfigRepository;
        public HordingTypeConfigService(ILogger<HordingTypeConfigService> logger, IHordingTypeConfigRepository hordingTypeConfigRepository)
        {
            _logger = logger;
            _hordingTypeConfigRepository = hordingTypeConfigRepository;
        }

        public async Task InsertUpdate(dtoHordingTypeConfig dtoHordingType, QueryExecutionMode mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("In_UserId", 1);
            parameters.Add("In_OrgId", dtoHordingType.OrgId);
            parameters.Add("In_HoardingTypeStr", dtoHordingType.HordingType);
            parameters.Add("In_Mode", (int)mode);
            // parameters.Add("In_Ipaddress", ipAddress);
            // parameters.Add("In_Source", source);

            await _hordingTypeConfigRepository.InsertUpdate(parameters);

        }

        public async Task<IEnumerable<dtoHordingTypeConfig>> GetActiveHoardingTypeConfigs()
        {

            var configs = await _hordingTypeConfigRepository.GetAllActive();

            return configs.Select(config => new dtoHordingTypeConfig
            {
                HordingType = config.HoardingType,
                HoardingConfigId = config.NUM_HOARDINGCONFIG_ID,
                HoardId = config.NUM_HOARDINGCONFIG_HOARDID,
                StatusFlag = config.VAR_HOARDINGCONFIG_ACTIVEFLAG
            });
        }

        public async Task<dtoHordingTypeConfig> GetHoardingTypeConfigById(int id)
        {

            var config = await _hordingTypeConfigRepository.GetById(id);

            return new dtoHordingTypeConfig
            {
                HordingType = config.HoardingType,
                HoardingConfigId = config.NUM_HOARDINGCONFIG_ID,
                HoardId = config.NUM_HOARDINGCONFIG_HOARDID,
                StatusFlag = config.VAR_HOARDINGCONFIG_ACTIVEFLAG
            };
        }
    }
}
