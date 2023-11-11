namespace MailLobbyer.SettingsProfilesClass
{
    public class SettingsProfiles
    {
        public Guid Id { get; }
        public string? ProfileName { get; set;}
        public string? SenderName { get; set; }
        public string? SenderEmail { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }

        public SettingsProfiles(Guid id, string profilename,string sendername, string senderemail, string username, string password, string host, int port)
        {
            Id = id;
            ProfileName = profilename;
            SenderName = sendername;
            SenderEmail = senderemail;
            Username = username;
            Password = password;
            Host = host;
            Port = port;
        }
    } 
}