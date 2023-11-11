using Microsoft.AspNetCore.SignalR;
using MailLobbyer.CSVFileClass;

namespace MailLobbyer.Server.Hubs;

public class CSVHub : Hub
{
    private static List<CSVFile> csvfilesinmemory = new List<CSVFile>();
    public void CSVFileSeeker()
    {
        string directorypath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MailLobbyer");

        if (!Directory.Exists(directorypath))
        {
            Directory.CreateDirectory(directorypath);
            System.Console.WriteLine("CSV contact-grouping directory created successfully.");
        }

        else
        {
            System.Console.WriteLine("CSV contact-grouping directory already exists.");
        }

        // Later on change to use config values set by the user
        string[] filepaths = Directory.GetFiles(directorypath);

        foreach (string filepath in filepaths)
        {
            CSVFile newcsvfile = new CSVFile(Path.GetFileNameWithoutExtension(filepath), filepath);
            csvfilesinmemory.Add(newcsvfile);
            System.Console.WriteLine(newcsvfile.Filename);
        }
    } 

    public async Task<List<CSVFile>> GetCSVFilesInMemory()
    {
        return csvfilesinmemory;
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        csvfilesinmemory.Clear();
        await base.OnDisconnectedAsync(exception);
    }
    
}
