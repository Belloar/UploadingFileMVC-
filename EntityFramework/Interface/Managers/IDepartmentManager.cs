using EntityProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityProject.Interface.Managers
{
    public interface IDepartmentManager
    {

        Department Create(Department department);

        List<Department> GetAll(bool withStudents = false);

        Department Get(int id, bool withStudents = false);

        Department Get(string name, bool withStudents = false);

        Department Update(Department department);

        void Delete(Department department);

        void Delete(int id);
    }
}
