using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
using MailLobbyer.SmtpClientSettingsComponent;

namespace MailLobbyer.EmailHandlerComponent
{
    public class EmailHandler
    {

        private readonly SmtpClientSettings _smtpSettings;
        public EmailHandler(IConfiguration configuration)
        {
            _smtpSettings = new SmtpClientSettings();
            configuration.GetSection("SmtpClientSettings").Bind(_smtpSettings);
        }

        public async Task SendEmailAsync(string EmailRecipient, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.Sendername, _smtpSettings.Senderemail));
            message.To.Add(new MailboxAddress("", EmailRecipient));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}