using app.advertise.DataAccess.Entities.Vendor;
using app.advertise.DataAccess.Repositories.Vendor;
using app.advertise.dtos;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.services.Interfaces;
using app.advertise.services.Vendor.Interfaces;
using Microsoft.Extensions.Logging;

namespace app.advertise.services.Vendor
{
    public class AppliDocService : IAppliDocService
    {
        private readonly VendorRequestHeaders _authData;
        private readonly IFileService _fileService;
        private readonly IAppliDocRepository _appliDocRepository;
        private readonly ILogger<AppliDocService> _logger;
        public AppliDocService(VendorRequestHeaders authData, IFileService fileService, IAppliDocRepository appliDocRepository, ILogger<AppliDocService> logger)
        {
            _authData = authData;
            _fileService = fileService;
            _appliDocRepository = appliDocRepository;
            _logger = logger;
        }

        public async Task InsertUpdateDoc(dtoFormFile file)
        {
            if (file.FormFile == null)
                throw new ApiException("File is required.", _logger);

            if (!(file.ApplicationId > 0))
                throw new ApiException("Application Id is required.", _logger);

            var existingDoc = await _appliDocRepository.GetSingleDoc(new AppliDoc() { NUM_APPLIDOC_APPID = file.ApplicationId, NUM_APPLIDOC_ULBID = _authData.UlbId });

            if (existingDoc == null)
                await InsertDoc(file);
            else
                await UpdateDoc(file, existingDoc);
        }
        private async Task UpdateDoc(dtoFormFile file, AppliDoc existingDoc)
        {


            var fileBytes = _fileService.ConvertToBytes(file.FormFile);

            var appliDoc = new AppliDoc()
            {
                BLO_APPLIDOC_IMAGE = fileBytes,
                VAR_APPLIDOC_UPDBY = _authData.UserId,
                NUM_APPLIDOC_ID = existingDoc.NUM_APPLIDOC_ID,
                NUM_APPLIDOC_APPID = existingDoc.NUM_APPLIDOC_APPID,
                NUM_APPLIDOC_ULBID = _authData.UlbId,
                VAR_APPLIDOC_APPLINO = existingDoc.VAR_APPLIDOC_APPLINO
            };

            await _appliDocRepository.UpdateDoc(appliDoc);
        }

        private async Task InsertDoc(dtoFormFile file)
        {
            if (file.FormFile == null)
                throw new ApiException("File is required.", _logger);

            if (!(file.ApplicationId > 0))
                throw new ApiException("Application Id is required.", _logger);

            if (string.IsNullOrEmpty(file.ApplicationNumber))
                throw new ApiException("App Number is required.", _logger);

            var fileBytes = _fileService.ConvertToBytes(file.FormFile);

            var appliDoc = new AppliDoc()
            {
                BLO_APPLIDOC_IMAGE = fileBytes,
                VAR_APPLIDOC_INSBY = _authData.UserId,
                NUM_APPLIDOC_APPID = file.ApplicationId,
                NUM_APPLIDOC_ULBID = _authData.UlbId,
                VAR_APPLIDOC_APPLINO = file.ApplicationNumber
            };

            await _appliDocRepository.InsertDoc(appliDoc);
        }

        public async Task<byte[]> GetFileBytes(dtoFormFile file)
        {
            var existingDoc = await _appliDocRepository.GetSingleDoc(new AppliDoc() { NUM_APPLIDOC_APPID = file.ApplicationId, NUM_APPLIDOC_ULBID = _authData.UlbId });

            if (existingDoc == null || existingDoc.BLO_APPLIDOC_IMAGE == null || existingDoc.BLO_APPLIDOC_IMAGE?.Length == 0)
                throw new ApiException("No image found.", _logger);

            return existingDoc.BLO_APPLIDOC_IMAGE;
        }
    }
}
