using EntityProject.Entities;
using EntityProject.Exceptions;
using EntityProject.Interface.Managers;
using EntityProject.Interface.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityProject.Implementations.Menu
{
    public class DepartmentMenu : IDepartmentMenu
    {

        private readonly IDepartmentManager departmentManager;

        public DepartmentMenu(IDepartmentManager manager)
        {
            departmentManager = manager;
        }

        public void Start()
        {
            bool continueMenu = true;

            do
            {

                Console.WriteLine("Welcome to Department Menu");

                Console.WriteLine("1. Add Department");
                Console.WriteLine("2. Get All Departments");
                Console.WriteLine("3. Get Department by Name");
                Console.WriteLine("4. Update Department");
                Console.WriteLine("5. Commot Department");
                Console.WriteLine("0. Back");

                int option = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        AddDepartment();
                        break;
                    case 2:
                        GetAllDepartments();
                        break;
                    case 3:
                        GetDepartment();
                        break;
                    case 4:
                        UpdateDepartment();
                        break;
                    case 5:
                        DeleteDepartment();
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

        private void DeleteDepartment()
        {
            Console.WriteLine("Enter department name: ");
            string name = Console.ReadLine();

            var department = departmentManager.Get(name);

            if (department == null)
            {
                Console.WriteLine("Department not found!");
                return;
            }

            Console.WriteLine("Are you sure you want to delete "+ name);

            departmentManager.Delete(department);
        }

        private void UpdateDepartment()
        {
            Console.WriteLine("Enter department name: ");
            string name = Console.ReadLine();

            var department = departmentManager.Get(name);

            if(department == null)
            {
                Console.WriteLine("Department not found!");
                return;
            }

            Console.WriteLine("1. Update name");
            Console.WriteLine("2. Update Hod");
            Console.WriteLine("3. Back");
            int option = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
            Console.WriteLine();

            switch(option)
            {
                case 1:
                    Console.WriteLine("Enter new name: ");
                    string newName = Console.ReadLine();
                    department.Name = newName;
                    departmentManager.Update(department);
                    break;
                case 2:
                    Console.WriteLine("Enter new HOD name: ");
                    string newHodName = Console.ReadLine();
                    department.DepartmentHOD = newHodName;
                    departmentManager.Update(department);
                    break;
            }

        }

        private void GetDepartment()
        {
            Console.WriteLine("Enter department name");
            string name = Console.ReadLine();
            Console.WriteLine("1. With Students");
            Console.WriteLine("2. Without Students");
            Console.WriteLine("0. Back");

            int option = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
            Console.WriteLine();

            var department = departmentManager.Get(name, option == 1);

            if(department == null)
            {
                Console.WriteLine("No department found");
                return;
            }

            PrintDepartment(department, withStudent: option == 1);
        }

        private void GetAllDepartments()
        {
            Console.WriteLine("1. With Students");
            Console.WriteLine("2. Without Students");
            Console.WriteLine("0. Back");

            int option = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
            Console.WriteLine();

            switch (option)
            {
                case 1:
                case 2:
                    var departments = departmentManager.GetAll(option == 1);

                    if (departments.Count == 0)
                    {
                        Console.WriteLine("No departments");
                        return;
                    }

                    int i = 1;

                    foreach (var department in departments)
                    {
                        PrintDepartment(department, i, option == 1);
                        i++;
                    }

                    break;
            }
        }

        private void AddDepartment()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter HOD Name: ");
            string hodName = Console.ReadLine();

            try
            {
                departmentManager.Create(
                                new Department
                                {
                                    Name = name,
                                    DepartmentHOD = hodName
                                });

                Console.WriteLine("Department created successfully");
            }

            catch (InvalidInputException e)
            {
                Console.WriteLine(e.Message);
                AddDepartment();
            }

            catch (InputExistException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        
        private void PrintDepartment(Department department, int count = 1, bool withStudent = false)
        {
            Console.WriteLine($"{count}. {department.Name} - Led By {department.DepartmentHOD}");

 
            if (!withStudent) return;
            if (department.Students.Count == 0)
            {
                Console.WriteLine("\tNo Students in this department");
                return;
            }

            int j = 1;

            foreach (var student in department.Students)
            {
                Console.WriteLine($"\t{j} {student.FirstName} {student.LastName}");
                j++;
            }
        }
    
    }
}
