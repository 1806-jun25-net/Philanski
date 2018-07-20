using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data
{
    public partial class Manager
    {
        public Manager()
        {
            TimeSheetApproval = new HashSet<TimeSheetApproval>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public ICollection<TimeSheetApproval> TimeSheetApproval { get; set; }
    }
}
