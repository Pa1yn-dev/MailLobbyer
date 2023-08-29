using MailLobbyer.ContactClass;
using MailLobbyer.FileUploadClass;

namespace MailLobbyer.EmailClass;

    public class Email 
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public FileUpload[] Attachments { get; set; }

        public Contact[] Selectedcontacts { get; set; }

    

        public Email(string subject, string body, FileUpload[] attachments, Contact[] selectedcontacts)
        {
            Subject = subject;
            Body = body;
            Attachments = attachments;
            Selectedcontacts = selectedcontacts;
        }
    }
