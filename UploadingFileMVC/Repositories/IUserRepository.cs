using UploadingFileMVC.Models;

namespace UploadingFileMVC.Repositories
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        User GetUser(int id);
        IList<User> GetUsers();
    }
}
