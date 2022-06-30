using EntityProject.Context;
using EntityProject.Implementations.Managers;
using EntityProject.Implementations.Menu;
using EntityProject.Interface.Managers;
using EntityProject.Interface.Menu;

namespace EntityProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentContext context = new StudentContext();

            IDepartmentManager departmentManager = new DepartmentManager(context);

            IDepartmentMenu departmentMenu = new DepartmentMenu(departmentManager);

            IMainMenu mainMenu = new MainMenu(departmentMenu);

            mainMenu.Start();
        }
    }
}