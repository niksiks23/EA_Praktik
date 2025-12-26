using System;

namespace DAL.Entities
{
    public class EquipmentHistory
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public DateTime ChangeDate { get; set; }
        public int? OldEmployeeId { get; set; }
        public int? NewEmployeeId { get; set; }
        public string Reason { get; set; } 
        public string Notes { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Employee OldEmployee { get; set; }
        public virtual Employee NewEmployee { get; set; }
    }
}