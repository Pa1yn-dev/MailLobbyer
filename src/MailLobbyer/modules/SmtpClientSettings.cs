namespace MailLobbyer.SmtpClientSettings
{
    public class SmtpClientSettings
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public bool UseStartTls { get; set; }
    }
}
