using app.advertise.services.Interfaces;

namespace app.advertise.services
{
    using app.advertise.dtos;
    using app.advertise.libraries.AppSettings;
    using app.advertise.libraries.Exceptions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.IO;

    public class FileService : IFileService
    {
        private readonly string _fileStorageBasePath;
        private readonly FileStorageSetting _fileSettings;
        private readonly ILogger<FileService> _logger;
        public FileService(IOptions<FileStorageSetting> fileSettings, ILogger<FileService> logger)
        {
            _fileSettings = fileSettings.Value;
            _fileStorageBasePath = _fileSettings.FilePath;
            _logger = logger;
        }

        public async Task WriteFile(dtoFormFile file)
        {
            try
            {
                var filePath = Path.Combine(_fileStorageBasePath, file.FileName);

                if (!Directory.Exists(_fileStorageBasePath))
                    Directory.CreateDirectory(_fileStorageBasePath);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.FormFile.CopyToAsync(stream);

            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, _logger);
            }
        }
    }

}
