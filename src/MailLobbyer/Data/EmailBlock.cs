using MailLobbyer.EmailClass;
using MailLobbyer.SettingsProfilesClass;

namespace MailLobbyer.EmailBlockClass
{
    public class EmailBlock
    {
        public Email[] NonScheduledEmails { get; set; }
        public SettingsProfiles SelectedProfile { get; set; }

        public EmailBlock(Email[] nonscheduledemails, SettingsProfiles selectedprofile)
        {
            NonScheduledEmails = nonscheduledemails;
            SelectedProfile = selectedprofile;
        }
    }
}


