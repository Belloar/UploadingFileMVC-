namespace EntityProject.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentHOD { get; set; }
        public List<Student> Students { get; set; }

    }
}
