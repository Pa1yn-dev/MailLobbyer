using MailLobbyer.ContactClass;
using MailLobbyer.FileUploadClass;
using MimeKit;

namespace MailLobbyer.EmailClass;

    public class Email 
    {
        public MimeMessage ProcessedEmailContents { get; set; }

        public Email(MimeMessage processedemailcontents)
        {
            ProcessedEmailContents = processedemailcontents;
        }
    }
