using System;

namespace BLL.DTOs
{
    public class EquipmentHistoryDTO
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentInventoryNumber { get; set; }
        public DateTime ChangeDate { get; set; }
        public int? OldEmployeeId { get; set; }
        public string OldEmployeeName { get; set; }
        public int? NewEmployeeId { get; set; }
        public string NewEmployeeName { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }

    public class EquipmentHistoryCreateDTO
    {
        public int EquipmentId { get; set; }
        public DateTime ChangeDate { get; set; }
        public int? OldEmployeeId { get; set; }
        public int? NewEmployeeId { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }
}