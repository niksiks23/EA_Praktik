using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class SoftwareLicense
    {
        public int Id { get; set; }
        public string SoftwareName { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string LicenseKey { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int LicenseCount { get; set; } 
        public string LicenseType { get; set; } 
        public decimal? PurchasePrice { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<InstalledSoftware> InstalledSoftware { get; set; } = new List<InstalledSoftware>();
    }
}