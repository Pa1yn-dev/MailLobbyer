namespace MailLobbyer.CSVCheckDirComponent
{
    public class CSVCheckDir
    {
        public void CSVDirChecker()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryName = "MailLobbyer";
            string directoryPath = Path.Combine(documentsPath, directoryName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine("Directory created successfully.");
            }
            else
            {
                Console.WriteLine("Directory already exists.");
            }
        }
        
    }
}