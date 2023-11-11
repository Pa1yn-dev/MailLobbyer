using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;


namespace MailLobbyer.CSVParserComponent
{
    public class CSVParser
    {
       public async Task CSVReader(string csvfilepath)
       {
        try
        {
            using (StreamReader reader = new StreamReader(csvfilepath))
            {
                //Skip headers used for XLSX reference before conversion to CSV
                reader.ReadLine();

                List<string> contactslist = new List<string>();

                while(!reader.EndOfStream)
                {
                    string currentline = await reader.ReadLineAsync();
                    System.Console.WriteLine(currentline);

                    contactslist.Add(currentline);
                }

            }
        }
        catch(Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
       }
    }
}