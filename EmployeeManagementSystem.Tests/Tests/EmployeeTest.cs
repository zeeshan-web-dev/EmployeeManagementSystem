using EmployeeManagementSystem.API.Controllers;
using EmployeeManagementSystem.DAL.Data.Interface;
using EmployeeManagementSystem.DTO.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Tests.Tests
{
    public class EmployeeTest
    {
        private Mock<IEmployeeRepository> employeeRepostiory;
        private List<EmployeeDto> employees;
        [SetUp]
        public void setup()
        {
            employeeRepostiory = new Mock<IEmployeeRepository>();
            employees = new List<EmployeeDto>();
            employees.Add(new EmployeeDto() { Id = 1, FullName = "zeeshan", EDateOfBirth = DateTime.Now, EDepartment = "physics", Email="zees@gmail.com"});
            employees.Add(new EmployeeDto() { Id = 2, FullName = "Arslan", EDateOfBirth = DateTime.Now, EDepartment = "Chemistry", Email = "arslan@gmail.com" });
            employees.Add(new EmployeeDto() { Id = 3, FullName = "Ahmar", EDateOfBirth = DateTime.Now, EDepartment = "Computer", Email = "ahmar@gmail.com" });
            employees.Add(new EmployeeDto() { Id = 4, FullName = "Asad", EDateOfBirth = DateTime.Now, EDepartment = "Maths", Email = "asad@gmail.com" });
        }
        [Test]
        public void GetEmployeesTest()
        {
            employeeRepostiory.Setup(e => e.List()).ReturnsAsync(employees.ToList());

            //Arrange
            var emplCont = new EmployeeController(employeeRepostiory.Object);
            var empList = emplCont.Get();

            // Arrest
            Assert.IsTrue(employees.Count > 0);
        }
        [Test]
        public void GetEmployeeTest()
        {
            employeeRepostiory.Setup(e => e.Read(5));

            //Arrange
            var emplCont = new EmployeeController(employeeRepostiory.Object);
            var emp = emplCont.Get(4);

            // Arrest
            Assert.IsTrue(emp != null);
        }
    }
}
