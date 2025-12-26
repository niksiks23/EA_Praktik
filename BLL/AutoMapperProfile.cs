using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Department
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentCreateDTO, Department>();
            CreateMap<DepartmentDTO, Department>();

            // Employee
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.Ignore());
            CreateMap<EmployeeCreateDTO, Employee>();

            // Equipment
            CreateMap<Equipment, EquipmentDTO>()
                .ForMember(dest => dest.EmployeeName, opt => opt.Ignore())
                .ForMember(dest => dest.EquipmentTypeName, opt => opt.Ignore());
            CreateMap<EquipmentCreateDTO, Equipment>();
            CreateMap<EquipmentDTO, Equipment>();
        }
    }
}