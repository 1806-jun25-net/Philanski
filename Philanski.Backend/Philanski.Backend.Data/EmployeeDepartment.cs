using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data
{
    public partial class EmployeeDepartment
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
    }
}
