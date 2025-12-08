using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeesManagement.WebApp.BLL.DTOs
{
    public class UpdatedDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }

        [Display(Name = "Date of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
