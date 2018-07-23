using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data.Models
{
    public partial class ManagerDepartments
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int DepartmentId { get; set; }
    }
}
