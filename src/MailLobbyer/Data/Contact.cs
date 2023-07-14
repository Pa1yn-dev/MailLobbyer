namespace MailLobbyer.ContactClass
{
    public class Contact
    {
        public string Prefix { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsSelected { get; set;}

        public Contact (string prefix, string surname, string forename, string email)
        {
            Prefix = prefix;
            Surname = surname;
            Forename = forename;
            Email = email;
        }
    }

    
}