using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Net;

namespace DollarInfo.Utils.EmailService
{
    public class EmailService
    {
        private readonly IAmazonSimpleEmailService _sesClient;

        public EmailService(IAmazonSimpleEmailService sesClient)
        {
            _sesClient = sesClient;
        }

        public async Task<bool> SendEmailAsync(string toAddress, string subject, string body)
        {
            var sendRequest = new SendEmailRequest
            {
                Source = Constants.EmailFrom,
                Destination = new Destination { ToAddresses = new List<string> { toAddress } },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = body
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = body
                        }
                    }
                }
            };

            try
            {
                var response = await _sesClient.SendEmailAsync(sendRequest);
                return response.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
}