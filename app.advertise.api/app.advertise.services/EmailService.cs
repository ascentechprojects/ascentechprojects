using app.advertise.dtos;
using app.advertise.libraries.AppSettings;
using app.advertise.libraries.Exceptions;
using app.advertise.services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace app.advertise.services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(dtoEmailBody dto)
        {
            try
            {
                var smtpClient = new SmtpClient(_settings.SmtpServer)
                {
                    Port = _settings.SmtpPort,
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = true,
                    UseDefaultCredentials = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail),
                    Subject = dto.Subject,
                    Body = dto.Body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(dto.To);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Failed to Send mail due to {ex.Message}", _logger);
            }

        }
    }
}
