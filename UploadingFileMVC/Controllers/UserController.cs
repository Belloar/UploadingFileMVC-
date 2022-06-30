using Microsoft.AspNetCore.Mvc;
using UploadingFileMVC.Models;
using UploadingFileMVC.Repositories;


namespace UploadingFileMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var users = _repository.GetUsers();
            return View(users);
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user,IFormFile file)
        {
            if(file != null)
            {
                string fileDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "files");
                Directory.CreateDirectory(fileDirectory);
                string contentType = file.ContentType.Split('/')[1];
                string fileName = $"{Guid.NewGuid()}.{contentType}";
                string fullPath = Path.Combine(fileDirectory, fileName);
                using(var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo (fileStream);
                }
                user.Picture = fileName;
            }
            _repository.AddUser(user);
            return View();
        }
        public IActionResult GetUser(int id)
        {
            _repository.GetUser(id);
            return View();

        }
        [HttpGet]
        public IActionResult GetUser()
        {
            
            return View();

        }
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var user = _repository.GetUser(id);
            return View(user);
        }


        [HttpPost]
        public IActionResult UpdateUser(int id, User updateUserRequest)
        {
            var user = _repository.GetUser(id);
            user.Name = updateUserRequest.Name;
            user.Picture = updateUserRequest.Picture;
            _repository.UpdateUser(user);
            return RedirectToAction("Index");

        }
        public IActionResult GetUsers()
        {
            var users = _repository.GetUsers();
            return View(users);
        }

    }
}
