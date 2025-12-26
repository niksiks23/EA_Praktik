using System;

namespace BLL.DTOs
{
    public class SoftwareLicenseDTO
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
        public int InstalledCount { get; set; }
        public int AvailableCount { get; set; }
        public bool IsExpired { get; set; }
        public bool IsExpiringSoon { get; set; }
        public string DisplayInfo => $"{SoftwareName} v{Version} ({Publisher}) - Доступно: {AvailableCount}";
    }

    public class SoftwareLicenseCreateDTO
    {
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
    }
}