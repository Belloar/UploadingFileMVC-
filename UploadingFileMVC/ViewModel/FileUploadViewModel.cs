using UploadingFileMVC.Models;

namespace UploadingFileMVC.ViewModel
{
    public class FileUploadViewModel
    {
        public List<FileSystem> FilesOnSystem { get; set; }
        public List<FileDatabase> FilesOnDatabase { get; set; }
    }
}
