using System;

namespace DAL.Entities
{
    public class InstalledSoftware
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int SoftwareLicenseId { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime? UninstallationDate { get; set; }
        public string InstallationPath { get; set; }
        public string Notes { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual SoftwareLicense SoftwareLicense { get; set; }
    }
}