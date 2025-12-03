using EmployeesManagement.WebApp.DAL.Models.Departments;
using EmployeesManagement.WebApp.DAL.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesManagement.WebApp.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> GetAll(bool withNoTracking = true)
        {
            if(withNoTracking)
                return _dbContext.Departments.AsNoTracking().ToList();

            return _dbContext.Departments.ToList();
        }

        public Department? Get(int id)
        {
            return _dbContext.Find<Department>(id);
        }

        public int Add(Department entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
