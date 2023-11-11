namespace MailLobbyer.FileUploadClass
{
    public class FileUpload
    {
        public string Filename { get; set; }
        public long Filesize { get; set; }
        public byte[] Filecontents { get; set; }

        public FileUpload(string filename, long filesize, byte[] filecontents)
        {
            Filename = filename;
            Filesize = filesize;
            Filecontents = filecontents;
        }
    }

}

