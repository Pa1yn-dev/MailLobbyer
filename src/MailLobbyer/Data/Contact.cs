namespace MailLobbyer.ContactClass
{
    public class Contact
    {
        public string Prefix { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public Contact (string prefix, string forename, string surname, string email)
        {
            Prefix = prefix;
            Forename = forename;
            Surname = surname;
            Email = email;
        }
    }

    
}