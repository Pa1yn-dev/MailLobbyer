using MailLobbyer.SettingsProfilesClass;
using MimeKit;

namespace MailLobbyer.EmailClass;

    public class Email 
    {
        public MimeMessage ProcessedEmailContents { get; set; }
        public SettingsProfiles SelectedProfile { get; set; }

        public Email(MimeMessage processedemailcontents, SettingsProfiles selectedprofile)
        {
            ProcessedEmailContents = processedemailcontents;
            SelectedProfile = selectedprofile;
        }
    }
