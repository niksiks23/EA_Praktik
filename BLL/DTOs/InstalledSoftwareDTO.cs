using System;

namespace BLL.DTOs
{
    public class InstalledSoftwareDTO
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentInventoryNumber { get; set; }
        public int SoftwareLicenseId { get; set; }
        public string SoftwareName { get; set; }
        public string SoftwareVersion { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime? UninstallationDate { get; set; }
        public string InstallationPath { get; set; }
        public string Notes { get; set; }
        public bool IsActive => !UninstallationDate.HasValue;
    }

    public class InstalledSoftwareCreateDTO
    {
        public int EquipmentId { get; set; }
        public int SoftwareLicenseId { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string InstallationPath { get; set; }
        public string Notes { get; set; }
    }
}