using System.Collections.Generic;

namespace DAL.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Head { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}