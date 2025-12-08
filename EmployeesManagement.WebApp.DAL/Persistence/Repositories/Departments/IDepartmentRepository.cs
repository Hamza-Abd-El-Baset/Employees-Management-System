using EmployeesManagement.WebApp.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeesManagement.WebApp.DAL.Persistence.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        Department? Get(int id);

        IEnumerable<Department> GetAll(bool withNoTracking = true);
        IQueryable<Department> GetAllAsIQueryable();

        int Add(Department entity);

        int Update(Department entity);

        int Delete(Department entity);

    }
}
