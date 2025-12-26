using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IEquipmentService
    {
        IEnumerable<EquipmentDTO> GetAll();
        EquipmentDTO GetById(int id);
        void Create(EquipmentCreateDTO dto);
        void Update(EquipmentDTO dto);
        void Delete(int id);
    }
}