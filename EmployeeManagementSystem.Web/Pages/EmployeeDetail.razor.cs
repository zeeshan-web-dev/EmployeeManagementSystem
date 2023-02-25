using EmployeeManagementSystem.DTO.DTO;
using EmployeeManagementSystem.Web.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagementSystem.Web.Pages
{
    public partial class EmployeeDetail: ComponentBase
    {
        [Parameter]
        public string? id { get; set; }
        [Inject]
        private EmployeeService employeeService { get; set; }
        private EmployeeDto Employee { get; set; } = new EmployeeDto();
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (id is not null)
                Employee = await employeeService.GetEmployeeAsync(Convert.ToInt32(id));
        }
    }
}
