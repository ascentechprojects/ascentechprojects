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
    public class HoardingMasterService : IHoardingMasterService
    {
        private readonly IHoardingMasterRepository _hoardingMasterRepository;
        private readonly UserRequestHeaders _authData;
        private readonly IUpdateStatusRespository _updateStatusRespository;
        private readonly ILogger<HoardingMasterService> _logger;
        public HoardingMasterService(IHoardingMasterRepository hoardingMasterRepository, UserRequestHeaders authData, IUpdateStatusRespository updateStatusRespository, ILogger<HoardingMasterService> logger)
        {
            _authData = authData;
            _hoardingMasterRepository = hoardingMasterRepository;
            _updateStatusRespository = updateStatusRespository;
            _logger= logger;
        }
        public async Task<IEnumerable<dtoHoardingMaster>> GetAll()
        {
            var result = await _hoardingMasterRepository.GetAll();

            return result.Select(record => new dtoHoardingMaster
            {
                Name = record.VAR_HORDING_HOLDNAME,
                Id = record.NUM_HORDING_ID,
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
                Ownership=record.VAR_HORDING_OWNERSHIP
            });
        }

        public async Task<dtoHoardingMaster> GetById(int id)
        {
            var result = await _hoardingMasterRepository.GetById(id);

            return new dtoHoardingMaster
            {
                ULBId = result.NUM_HORDING_ULBID,
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
                Ownership = result.VAR_HORDING_OWNERSHIP
            };
        }

        public async Task InsertUpdate(dtoHoardingMaster dtoRequest, QueryExecutionMode mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("in_ULBID", dtoRequest.ULBId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_UserId", _authData.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("in_holdid", dtoRequest.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_holdname", dtoRequest.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("in_holdaddress", dtoRequest.Address, DbType.String, ParameterDirection.Input);
            parameters.Add("in_holdtype", dtoRequest.DisplayTypeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_ownership", dtoRequest.Ownership, DbType.String, ParameterDirection.Input);
            parameters.Add("in_disptypeid", dtoRequest.DisplayTypeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_prabhagid", dtoRequest.PrabhagId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_locationid", dtoRequest.LocationId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_landmark", dtoRequest.Landmark, DbType.String, ParameterDirection.Input);
            parameters.Add("in_length", dtoRequest.Length, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("in_width", dtoRequest.Width, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("in_totalsqft", dtoRequest.TotalSQFT, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("in_active", RecordStatus.A.ToString(), DbType.String, ParameterDirection.Input);
            parameters.Add("in_ipaddress", _authData.IpAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("in_source", _authData.Source, DbType.String, ParameterDirection.Input);
            parameters.Add("in_mode", (int)mode, DbType.Int32, ParameterDirection.Input);
            parameters.Add("in_Userid", _authData.UserId, DbType.String, ParameterDirection.Input);

            await _hoardingMasterRepository.InsertUpdate(parameters);
        }
        public async Task ModifyStatusById(int id)
        {
           var existingRecord= await _hoardingMasterRepository.GetById(id) ?? throw new ApiException(AppConstants.Msg_RecordNotFound, _logger);

            existingRecord.VAR_HORDING_ACTIVE= existingRecord.VAR_HORDING_ACTIVE.ToggleStatus();
            existingRecord.DAT_HORDING_UPDT=DateTime.Now;
            existingRecord.VAR_HORDING_UPDBY=_authData.UserId;
            
            await _updateStatusRespository.UpdateStatus(EntityType.HoardingMaster, existingRecord);
        }
    }
}
