using Microsoft.AspNetCore.SignalR;
using MailLobbyer.ContactClass;
using MailLobbyer.FileUploadClass;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MailLobbyer.EmailClass;
using MailLobbyer.SettingsProfilesClass;

namespace MailLobbyer.Server.Hubs;

public class EmailHub : Hub
{
    private static int sentemails = 0;

    public async Task EmailHandler(string subject, string body, Contact contact, List<FileUpload> fileuploads, SettingsProfiles profile)
    {
        await Task.Run(async () =>
        {
            if (sentemails >= 20)
            {
                // If 20 emails are sent, wait 5 minutes.
                await Task.Delay(300000);
                sentemails = 0;
            }

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

                       var message = new MimeMessage();
            message.From.Add(new MailboxAddress(profile.SenderName, profile.SenderEmail));
            message.To.Add(new MailboxAddress(string.Concat(contact.Forename, " ", contact.Surname), contact.Email));
            message.Subject = subject;

            var builder = new BodyBuilder();

            builder.TextBody = body;

            if(fileuploads.Any())
            {
                foreach (FileUpload fileupload in fileuploads)
                {
                    builder.Attachments.Add(fileupload.Filename, fileupload.Filecontents);
                }
            }

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(profile.Host, profile.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(profile.Username, profile.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            lock(this)
            {
                sentemails++;
            }

            
            
        });
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
    
}
