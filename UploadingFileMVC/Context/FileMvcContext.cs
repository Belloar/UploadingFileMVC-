using Microsoft.EntityFrameworkCore;
using UploadingFileMVC.Models;

namespace UploadingFileMVC.Context
{
    public class FileMvcContext : DbContext
    {
        public DbSet<User> Users {get;set;}
        public DbSet<FileSystem> FileSystem {get;set;}
        public DbSet<FileDatabase> FileDatabase {get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server = localhost; Database = FileUploadDB; Uid = root; Pwd = @belloAR2001; ", new MySqlServerVersion(
            new Version(8, 0, 29)));

        }
    }
}
