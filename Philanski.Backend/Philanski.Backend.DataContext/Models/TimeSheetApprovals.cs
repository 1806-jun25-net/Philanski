using System;
using System.Collections.Generic;

namespace Philanski.Backend.DataContext.Models
{
    public partial class TimeSheetApprovals
    {
        public int Id { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public decimal WeekTotalRegular { get; set; }
        public string Status { get; set; }
        public int? ApprovingManagerId { get; set; }
        public DateTime TimeSubmitted { get; set; }
        public int EmployeeId { get; set; }

        public Managers ApprovingManager { get; set; }
        public Employees Employee { get; set; }
    }
}
