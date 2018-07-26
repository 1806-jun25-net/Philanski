using System;
using System.Collections.Generic;

namespace Philanski.Backend.DataContext.Models
{
    public partial class Managers
    {
        public Managers()
        {
            ManagerDepartments = new HashSet<ManagerDepartments>();
            TimeSheetApprovals = new HashSet<TimeSheetApprovals>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public Employees Employee { get; set; }
        public ICollection<ManagerDepartments> ManagerDepartments { get; set; }
        public ICollection<TimeSheetApprovals> TimeSheetApprovals { get; set; }
    }
}
