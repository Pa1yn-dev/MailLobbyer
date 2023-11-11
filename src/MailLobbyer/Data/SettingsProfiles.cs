namespace MailLobbyer.SettingsProfilesClass
{
    public class SettingsProfiles
    {
        public string? SenderName { get; set; }
        public string? SenderEmail { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }

        public SettingsProfiles(string sendername, string senderemail, string username, string password, string host, int port)
        {
            SenderName = sendername;
            SenderEmail = senderemail;
            Username = username;
            Password = password;
            Host = host;
            Port = port;
        }
    } 
}