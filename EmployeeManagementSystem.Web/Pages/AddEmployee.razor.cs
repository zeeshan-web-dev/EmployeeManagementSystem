using EmployeeManagementSystem.DTO.DTO;
using EmployeeManagementSystem.Web.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Data;

namespace EmployeeManagementSystem.Web.Pages
{
    public partial class AddEmployee: ComponentBase
    {
        [Parameter]
        public string? id { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private EmployeeService employeeService { get; set; }
        private EmployeeDto Employee { get; set; } = new EmployeeDto();
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (id is not null)
                Employee = await employeeService.GetEmployeeAsync(Convert.ToInt32(id));
        }
        private async Task AddOrUpdateEmployeeAsync()
        {
            await employeeService.AddOrUpdateEmployeeAsync(Employee);
            navigationManager.NavigateTo("/");
        }
    }
}
