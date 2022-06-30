using EntityProject.Interface.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityProject.Implementations.Menu
{
    public class MainMenu :  IMainMenu
    {

        private readonly IDepartmentMenu departmentMenu;

        public MainMenu(IDepartmentMenu deptMenu)
        {
            departmentMenu = deptMenu;
        }

        public void Start()
        {
            bool continueMenu = true;

            do
            {

                
                Console.WriteLine("Welcome to Olohunlomerue Comprehensive Low School");

                Console.WriteLine("1. Department Menu");
                Console.WriteLine("2. Student Menu");
                Console.WriteLine("0. Exit");

                int option = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        departmentMenu.Start();
                        break;
                    case 2:
                        break;
                    case 0:
                        continueMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

            } while (continueMenu);
        }
    }
}
