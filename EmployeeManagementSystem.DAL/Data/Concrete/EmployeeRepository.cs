using EmployeeManagementSystem.DAL.Data.Interface;
using EmployeeManagementSystem.DAL.Entities;
using EmployeeManagementSystem.DTO.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace EmployeeManagementSystem.DAL.Data.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeRepository(EmployeeContext employeeContext) => _employeeContext = employeeContext;
        public async Task<bool> Create(EmployeeDto emp)
        { 
            await _employeeContext.Employees.AddAsync(new Employee { Name = emp.FullName, DateOfBirth = emp.EDateOfBirth, Email = emp.Email, Department = emp.EDepartment });
            return await _employeeContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            Employee employee = await _employeeContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee is null)
                return false;
            
                _employeeContext.Employees.Remove(employee);
            return await _employeeContext.SaveChangesAsync() > 0;
        }

        public async Task<List<EmployeeDto>> List()
        {
            List<EmployeeDto> list = new List<EmployeeDto>();
            foreach (Employee e in await _employeeContext.Employees.ToListAsync())
            {
                list.Add(new EmployeeDto { Id = e.Id ,FullName = e.Name, EDateOfBirth = e.DateOfBirth, EDepartment = e.Department, Email = e.Email });
            }
            return list;
        }

            
        public async Task<EmployeeDto> Read(int id) =>
            await _employeeContext.Employees.Where(e => e.Id == id)
            .Select( e => new EmployeeDto { FullName = e.Name, EDateOfBirth = e.DateOfBirth, EDepartment = e.Department, Email = e.Email, Id = e.Id })
            .FirstOrDefaultAsync();

        public async Task<bool> Update(EmployeeDto emp)
        {
            Employee employee = await _employeeContext.Employees.FirstOrDefaultAsync(e => e.Id == emp.Id);
            if (employee is null)
                return false;
            employee.Name = emp.FullName;
            employee.DateOfBirth = emp.EDateOfBirth;
            employee.Department = emp.EDepartment;
            employee.Email = emp.Email;

            return await _employeeContext.SaveChangesAsync() > 0;
        }
    }
}
