using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using MailLobbyer.ContactClass;
using MailLobbyer.CSVFileClass;

namespace MailLobbyer.CSVServiceComponent
{
    public class CSVService
    {
        public List<Contact> contacts = new List<Contact>();

        public async Task CSVParser(string csvfilepath)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (StreamReader reader = new StreamReader(csvfilepath))
                    {
                        string currentline;

                        //Skip headers used for XLSX reference before conversion to CSV
                        reader.ReadLine();

                        while((currentline = reader.ReadLine()) != null)
                        {
                            string[] substrings = currentline.Split(',');
                    
                            Contact newcontact = new Contact(substrings[0], substrings [1], substrings[2], substrings[3]);
                            contacts.Add(newcontact);
                    }

                    }
                }
                catch(Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }

            });        
       }
    }
}