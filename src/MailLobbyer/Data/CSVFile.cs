namespace MailLobbyer.CSVFileClass
{
    public class CSVFile
    {
        public string Filename { get; set; }
        public string Filepath {get; set; }

        public CSVFile (string filename, string filepath)
        {
            Filename = filename;
            Filepath = filepath;
        }
    }
}