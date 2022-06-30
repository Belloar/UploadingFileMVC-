using UploadingFileMVC.Context;
using UploadingFileMVC.Models;

namespace UploadingFileMVC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FileMvcContext _context;

        public UserRepository(FileMvcContext context)
        {
            _context = context;
        }

        public bool AddUser(User user)
        {
            var NewUser = new User
            {
                Name = user.Name,
                Picture = user.Picture
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return _context.Users.Any(u => u.Name.ToLower() == user.Name.ToLower());
        }

        public IList<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public bool UpdateUser(User user)
        {
           _context.Users.Update(user);
            _context.SaveChanges();
            return _context.Users.Any(u => u.Name.ToLower() == user.Name.ToLower());
        }
        public User[] GetAllUsers()
        {
             return _context.Users.ToArray();
        }
    }
}

