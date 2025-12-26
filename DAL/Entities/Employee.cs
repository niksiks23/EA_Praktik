using System.Collections.Generic;

namespace DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
        public virtual ICollection<EquipmentHistory> EquipmentHistoriesAsOld { get; set; } = new List<EquipmentHistory>();
        public virtual ICollection<EquipmentHistory> EquipmentHistoriesAsNew { get; set; } = new List<EquipmentHistory>();
    }
}