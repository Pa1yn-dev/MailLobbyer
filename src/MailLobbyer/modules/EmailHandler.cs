using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
using MailLobbyer.SmtpClientSettings;

namespace MailLobbyer.EmailHandler
{
    public class EmailHandler
    {

        // Constructor that accepts SMTP client settings upon creating an instance of EmailHandler, accepts parameter of SmtpClientSettings.SmtpClientSettings object as smtpsettings
        public EmailHandler(SmtpClientSettings.SmtpClientSettings smtpsettings)
        {
            _smtpsettings = smtpsettings;
        }

        // The SMTP client settings provided during object creation, value passed as smtpsettings is assigned to the _smtpsettings
        private readonly SmtpClientSettings.SmtpClientSettings _smtpsettings;

        // Method to send an email asynchronously
        public async Task SendEmailAsync(string EmailRecipient, string subject, string body)
        {
            // Create a new MimeMessage for the email
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender name", "Sender email"));
            message.To.Add(new MailboxAddress("", EmailRecipient));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };
    
            // Create a new SmtpClient to send the email
            using (var client = new SmtpClient())
            {
                // Connect to the SMTP server
                await client.ConnectAsync(_smtpsettings.Host, _smtpsettings.Port, SecureSocketOptions.StartTls);
                
                // Authenticate with the SMTP server using the provided settings
                await client.AuthenticateAsync(_smtpsettings.Username, _smtpsettings.Password);
                
                // Send the email
                await client.SendAsync(message);
                
                // Disconnect from the SMTP server
                await client.DisconnectAsync(true);
            }
        }
    }
}
