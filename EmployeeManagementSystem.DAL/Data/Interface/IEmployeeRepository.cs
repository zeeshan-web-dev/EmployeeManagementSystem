using EmployeeManagementSystem.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DAL.Data.Interface
{
    public interface IEmployeeRepository
    {
        Task<bool> Create(EmployeeDto emp);
        Task<bool> Update(EmployeeDto emp);
        Task<EmployeeDto> Read(int id);
        Task<bool> Delete(int id);
        Task<List<EmployeeDto>> List();

    }
}
