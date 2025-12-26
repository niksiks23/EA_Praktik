using System;

namespace BLL.Interfaces
{
    public interface IObservableEquipmentService
    {
        void Subscribe(IEquipmentObserver observer);
        void Unsubscribe(IEquipmentObserver observer);
        void NotifyEquipmentChanged();
        void NotifyEmployeeChanged();
        void NotifyEquipmentTypeChanged();
    }
}