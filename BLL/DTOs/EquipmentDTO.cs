using System;

namespace BLL.DTOs
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        public string Name { get; set; }
        public int EquipmentTypeId { get; set; }
        public string EquipmentTypeName { get; set; }
        public string SerialNumber { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string Specifications { get; set; }
        public string DisplayInfo => $"{InventoryNumber} - {Name}";
    }

    public class EquipmentCreateDTO
    {
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
        public string DisplayInfo => $"{InventoryNumber} - {Name}";
    }
}