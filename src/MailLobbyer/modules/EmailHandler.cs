using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
using MailLobbyer.SmtpClientSettingsComponent;
using MailLobbyer.ContactClass;

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

        public async Task EmailSyntaxHandler(string subject, string body, string exclude, Contact contact)
        {
            string prefixsyntax = "/P";
            string fullnamesyntax = "/FN";
            string forenamesyntax = "/F";
            string surnamesyntax = "/S";

            subject = subject.Replace(prefixsyntax, contact.Prefix, StringComparison.OrdinalIgnoreCase)
                            .Replace(fullnamesyntax, string.Concat(contact.Forename, " ", contact.Surname), StringComparison.OrdinalIgnoreCase)
                            .Replace(forenamesyntax, contact.Forename, StringComparison.OrdinalIgnoreCase)
                            .Replace(surnamesyntax, contact.Surname, StringComparison.OrdinalIgnoreCase);
            
            body = body.Replace(prefixsyntax, contact.Prefix, StringComparison.OrdinalIgnoreCase)
                            .Replace(fullnamesyntax, string.Concat(contact.Forename, " ", contact.Surname), StringComparison.OrdinalIgnoreCase)
                            .Replace(forenamesyntax, contact.Forename, StringComparison.OrdinalIgnoreCase)
                            .Replace(surnamesyntax, contact.Surname, StringComparison.OrdinalIgnoreCase);
            
            await SendEmailAsync(string.Concat(contact.Forename, " ", contact.Surname),contact.Email,subject,body);
        }

        public async Task SendEmailAsync(string RecipientName, string RecipientEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.Sendername, _smtpSettings.Senderemail));
            message.To.Add(new MailboxAddress(RecipientName, RecipientEmail));
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