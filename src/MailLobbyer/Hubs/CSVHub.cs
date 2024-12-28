using Microsoft.AspNetCore.SignalR;
using MailLobbyer.CSVFileClass;
using MailLobbyer.ContactClass;
using Microsoft.AspNetCore.Components.Forms;

namespace MailLobbyer.Server.Hubs;

public class CSVHub : Hub
{
    private static List<CSVFile> csvfilesinmemory = new List<CSVFile>();
    private static string[] filepaths;

    public async Task CSVFileSeeker()
    {
        string directorypath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MailLobbyer");

        if (!Directory.Exists(directorypath))
        {
            Directory.CreateDirectory(directorypath);
        }

        // Later on change to use config values set by the user
        filepaths = Directory.GetFiles(directorypath);

        foreach (string filepath in filepaths)
        {
            using (var ms = new MemoryStream())
            {
                using(FileStream fs = new FileStream(filepath, FileMode.Open,FileAccess.Read))
                {
                    await fs.CopyToAsync(ms);
                }

                byte[] filecontents = ms.ToArray();
                CSVFile newcsvfile = new CSVFile(Guid.NewGuid(),Path.GetFileNameWithoutExtension(filepath), filepath, filecontents);
                csvfilesinmemory.Add(newcsvfile);
            }
            
        }
    }

    public async Task CSVFilesToUpload(String fileName, byte[] fileBytes)
    {
        string directorypath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MailLobbyer");

        if (!Directory.Exists(directorypath))
        {
            Directory.CreateDirectory(directorypath);
        }

        try
        {
            string filepath = Path.Combine(directorypath, fileName);
            await File.WriteAllBytesAsync(filepath, fileBytes);
            Console.WriteLine("File saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
        
    }

    public async Task RemoveCSVFile(string selectedcsvfilename)
    {
        foreach (string filepath in filepaths)
        {
            if(Path.GetFileNameWithoutExtension(filepath) == selectedcsvfilename)
            {
                File.Delete(filepath);
            }
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
