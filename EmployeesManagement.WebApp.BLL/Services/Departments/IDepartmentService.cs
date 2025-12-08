using EmployeesManagement.WebApp.BLL.DTOs;
using EmployeesManagement.WebApp.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesManagement.WebApp.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentsById(int id);
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);

    }
}
