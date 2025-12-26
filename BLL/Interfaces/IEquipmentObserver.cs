using System;

namespace BLL.Interfaces
{
    public interface IEquipmentObserver
    {
        void OnEquipmentChanged();
        void OnEmployeeChanged();
        void OnEquipmentTypeChanged();
    }
}