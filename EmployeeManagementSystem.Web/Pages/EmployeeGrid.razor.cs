using EmployeeManagementSystem.DTO.DTO;
using EmployeeManagementSystem.Web.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Web.Pages
{
    public partial class EmployeeGrid: ComponentBase
    {
        private List<EmployeeDto> employees { get; set; } = new List<EmployeeDto>();
        private List<EmployeeDto> duplicateEmployees { get; set; } = new List<EmployeeDto>();
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private EmployeeService employeeService { get; set; }
        private string searchText { get; set; } = "";
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await duplicateEmployee();
        }
        private async void AddRow() => navigationManager.NavigateTo("/add-employee");

        private async void EditEmployeeAsync(int id) => navigationManager.NavigateTo("/edit-employee/" + id);

        private async void EmployeeDetail(int id) => navigationManager.NavigateTo("/detail-employee/" + id);
        private async void DeleteEmployeeAsync(int id)
        {
            await employeeService.DeleteEmployeeAsync(id);
            await duplicateEmployee();
        }
        private async void SearchEmployeeAsync()
        {
            employees = duplicateEmployees.Where(e => e.FullName.ToLower().Contains(searchText) || e.Email.ToLower().Contains(searchText) 
            || e.EDepartment.ToLower().Contains(searchText)).ToList();    
        }
        private async Task duplicateEmployee()
        {
            duplicateEmployees.Clear();
            employees.Clear();
            employees = await employeeService.GetEmployeesAsync();
            string json = JsonConvert.SerializeObject(employees);
            duplicateEmployees = JsonConvert.DeserializeObject<List<EmployeeDto>>(json);
            StateHasChanged();
        }
    }
}
