using System;
using System.Collections.Generic;

namespace Philanski.Backend.DataContext.Models
{
    public partial class Departments
    {
        public Departments()
        {
            EmployeeDepartments = new HashSet<EmployeeDepartments>();
            ManagerDepartments = new HashSet<ManagerDepartments>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeeDepartments> EmployeeDepartments { get; set; }
        public ICollection<ManagerDepartments> ManagerDepartments { get; set; }
    }
}
