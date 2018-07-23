using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data
{
    public partial class Managers
    {
        public Managers()
        {
            TimeSheetApprovals = new HashSet<TimeSheetApprovals>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public Employees Employee { get; set; }
        public ICollection<TimeSheetApprovals> TimeSheetApprovals { get; set; }
    }
}
