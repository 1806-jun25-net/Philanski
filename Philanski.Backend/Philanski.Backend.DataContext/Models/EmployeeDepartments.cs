using System;
using System.Collections.Generic;

namespace Philanski.Backend.DataContext.Models
{
    public partial class EmployeeDepartments
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }

        public Departments Department { get; set; }
        public Employees Employee { get; set; }
    }
}
