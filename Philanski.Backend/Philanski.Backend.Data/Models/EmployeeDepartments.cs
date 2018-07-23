using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data.Models
{
    public partial class EmployeeDepartments
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
    }
}
