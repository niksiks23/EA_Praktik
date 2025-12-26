using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        public string Name { get; set; }
        public int EquipmentTypeId { get; set; }
        public string SerialNumber { get; set; }
        public int? EmployeeId { get; set; } 
        public DateTime RegistrationDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string Status { get; set; } 
        public string Location { get; set; }
        public string Specifications { get; set; }

        public virtual EquipmentType EquipmentType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<EquipmentHistory> EquipmentHistories { get; set; } = new List<EquipmentHistory>();
        public virtual ICollection<InstalledSoftware> InstalledSoftware { get; set; } = new List<InstalledSoftware>();
    }
}