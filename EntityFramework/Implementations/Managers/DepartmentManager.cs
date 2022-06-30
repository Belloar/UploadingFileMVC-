using EntityProject.Context;
using EntityProject.Entities;
using EntityProject.Exceptions;
using EntityProject.Interface.Managers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityProject.Implementations.Managers
{
    public class DepartmentManager : IDepartmentManager
    {

        private readonly StudentContext context;

     
        public DepartmentManager(StudentContext studentContext)
        {

            context = studentContext;

        }

        public Department Create(Department department)
        {

            if (department.Name == "" || department.DepartmentHOD == "") throw new InvalidInputException("Please enter a value");

            if (department.Name.Length < 5) throw new InvalidInputException("Department name length must be > 5");

            if (CheckDeptExist(department)) 
                throw new InputExistException("Department laready exists");



            context.Departments.Add(department);
            context.SaveChanges();

            return department;
            
        }

        public List<Department> GetAll(bool withStudents = false)
        {
            return withStudents ? context.Departments.Include(d => d.Students).ToList() : context.Departments.ToList();
        }

        public bool CheckDeptExist(Department department)
        {
            return context.Departments.Any(d => d.Name.ToLower() == department.Name.ToLower());
        }

        public Department Get(int id, bool withStudents = false)
        {
            return withStudents ? 
                context.Departments.Include(d => d.Students).SingleOrDefault(d => d.Id==id) 
                : context.Departments.Find(id);
        }

        public Department Get(string name, bool withStudents = false)
        {
            return withStudents
                ? context.Departments.Include(d => d.Students).SingleOrDefault(d => d.Name.ToLower() == name.ToLower())
                : context.Departments.FirstOrDefault(d => d.Name.ToLower() == name.ToLower());
        }

        public Department Update(Department department)
        {
            context.Departments.Update(department);
            context.SaveChanges();
            return department;
        }

        public void Delete(Department department)
        {
            context.Departments.Remove(department);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var department = Get(id);

            if(department != null)
            {
                context.Departments.Remove(department);
                context.SaveChanges();
            }
        }
    }
}
