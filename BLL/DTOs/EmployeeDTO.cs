namespace BLL.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } 
    }

    public class EmployeeCreateDTO
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
    }
}