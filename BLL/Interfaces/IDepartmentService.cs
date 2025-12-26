using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDTO> GetAll();
        DepartmentDTO GetById(int id);
        void Create(DepartmentCreateDTO dto);
        void Update(DepartmentDTO dto);
        void Delete(int id);
    }
}