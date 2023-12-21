using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext appDbContext;

        public EmployeeRepository(EmployeeContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await appDbContext.Employee.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await appDbContext.Employee
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await appDbContext.Employee.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await appDbContext.Employee
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.DateOfBrith = employee.DateOfBrith;
                result.Gender = employee.Gender;
                result.DepartmentId = employee.DepartmentId;
                result.PhotoPath = employee.PhotoPath;

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async void DeleteEmployee(int employeeId)
        {
            var result = await appDbContext.Employee
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (result != null)
            {
                appDbContext.Employee.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}