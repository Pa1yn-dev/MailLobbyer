namespace MailLobbyer.CSVFileClass
{
    public class CSVFile
    {
        public Guid Id { get; }
        public string Filename { get; set; }
        public string Filepath { get; set; }
        public byte[] Filecontents { get; set; }

        public CSVFile (Guid id, string filename, string filepath, byte[] filecontents)
        {
            Id = id;
            Filename = filename;
            Filepath = filepath;
            Filecontents = filecontents;
        }
    }
}