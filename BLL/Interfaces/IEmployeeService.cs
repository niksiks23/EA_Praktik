using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> GetAll();
        EmployeeDTO GetById(int id);
        void Create(EmployeeCreateDTO dto);
        void Update(EmployeeDTO dto);
        void Delete(int id);
    }
}