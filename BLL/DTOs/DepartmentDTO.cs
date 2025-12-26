namespace BLL.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Head { get; set; }
    }

    public class DepartmentCreateDTO
    {
        public string Name { get; set; }
        public string Head { get; set; }
    }
}