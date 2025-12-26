using BLL.DTOs;
using BLL.Services;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IInstalledSoftwareService
    {
        List<InstalledSoftwareDTO> GetAll();
        InstalledSoftwareDTO GetById(int id);
        List<InstalledSoftwareDTO> GetByEquipmentId(int equipmentId);
        List<InstalledSoftwareDTO> GetActiveByEquipmentId(int equipmentId);
        List<InstalledSoftwareDTO> GetBySoftwareLicenseId(int softwareLicenseId);
        void InstallSoftware(InstalledSoftwareCreateDTO dto);
        void UninstallSoftware(int id);
        void Delete(int id);
        void Update(InstalledSoftwareDTO dto);
        bool IsSoftwareInstalled(int equipmentId, int softwareLicenseId);
        Dictionary<string, object> GetStatistics();
        List<SoftwareLicenseDTO> GetAvailableSoftwareForEquipment(int equipmentId);
        List<LicenseUsageReportDTO> GetLicenseUsageReport();
    }
}