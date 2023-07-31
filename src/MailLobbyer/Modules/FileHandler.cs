using Microsoft.AspNetCore.Components.Forms;
using MailLobbyer.FileUploadClass;

namespace MailLobbyer.FileHandlerComponent
{
    public class FileHandler
    {
        private const long MaxAllowedFileSize = 26214400;
        public List<FileUpload> fileuploads = new List<FileUpload>();

        public async Task ExtractUploadedFileContents(List<IBrowserFile> selectedfiles)
        {
            if(selectedfiles.Count > 0)
            {
                using (var ms = new MemoryStream())
                {
                    foreach (var file in selectedfiles)
                    {
                        await file.OpenReadStream(MaxAllowedFileSize).CopyToAsync(ms);
                        byte[] filecontents = ms.ToArray();
                        FileUpload fileupload = new FileUpload(file.Name, file.Size, filecontents);
                        fileuploads.Add(fileupload);
                    }

                }
                
            }
        }

    }
}
