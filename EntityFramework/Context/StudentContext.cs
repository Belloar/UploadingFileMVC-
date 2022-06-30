using EntityProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityProject.Context
{
    public class StudentContext : DbContext
    {

        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server = localhost; Database = EntityProjectDB; Uid = root; Pwd = @belloAR2001; ");
        }
    }
}
