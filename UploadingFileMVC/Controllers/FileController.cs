using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UploadingFileMVC.Context;
using UploadingFileMVC.Models;
using UploadingFileMVC.ViewModel;

namespace UploadingFileMVC.Controllers
{
    public class FileController : Controller
    {
        public FileMvcContext _context;
        public FileController(FileMvcContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           var files =LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(files);
        }
        private  FileUploadViewModel LoadAllFiles()
        {
            var viewModel = new FileUploadViewModel();
            viewModel.FilesOnDatabase =  _context.FileDatabase.ToList();
            viewModel.FilesOnSystem =  _context.FileSystem.ToList();
            return viewModel;
        }

        [HttpPost]
        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fileModel = new FileSystem
                    {
                        CreatedOn = DateTime.UtcNow,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        Description = description,
                        FilePath = filePath
                    };
                    _context.FileSystem.Add(fileModel);
                    _context.SaveChanges();
                }
            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }
    }
}
