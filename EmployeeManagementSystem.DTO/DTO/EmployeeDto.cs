﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DTO.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime EDateOfBirth { get; set; }
        public string? EDepartment { get; set; }
    }
}
