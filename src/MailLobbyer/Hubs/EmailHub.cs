using Microsoft.AspNetCore.SignalR;
using MailLobbyer.ContactClass;
using MailLobbyer.FileUploadClass;
using MailLobbyer.SmtpClientSettingsComponent;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MailLobbyer.EmailClass;

namespace MailLobbyer.Server.Hubs;

public class EmailHub : Hub
{
    private List<Email> nonscheduledemails = new List<Email>();

    public async Task EmailHandler(string subject, string body, Contact contact, List<FileUpload> fileuploads)
    {
        await Task.Run(async () =>
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

                       var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Test", "Test@gmail.com"));
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








            //message.Body = new TextPart("plain") { Text = body };
            /*
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("peterhamilton522@gmail.com", "okakgvqorczjjllu");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                Console.WriteLine("Sent");
            }
            */
        });
    
        

    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
    
}
