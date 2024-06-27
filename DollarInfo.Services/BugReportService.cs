using DollarInfo.DAL.Models;
using DollarInfo.Services.Interfaces;
using DollarInfo.Utils;
using DollarInfo.Utils.EmailService;

namespace DollarInfo.Services
{
    public class BugReportService : IBugReportService
    {
        private readonly EmailService _emailService;

        public BugReportService(EmailService emailService) => _emailService = emailService;

        public async Task Post(BugReportRequest request)
        {
            try
            {
                string emailBody = Constants.BugReportBody
                    .Replace("{{UserName}}", request.Name)
                    .Replace("{{UserEmail}}", request.EmailFrom)
                    .Replace("{{BugDescription}}", request.Description)
                    .Replace("{{CurrentDate}}", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));

                await _emailService.SendEmailAsync(Constants.EmailFrom, Constants.Subject, emailBody);
            }
            catch (Exception e)
            {
                throw new Exception($"Error sending email. Error description: {e.Message}");
            }
        }
    }
}