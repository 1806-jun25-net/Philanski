using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data
{
    public partial class TimeSheetApproval
    {
        public int Id { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public decimal WeekTotalRegular { get; set; }
        public string Status { get; set; }
        public int? ApprovingManagerId { get; set; }
        public DateTime TimeSubmitted { get; set; }
        public int EmployeeId { get; set; }

        public Manager ApprovingManager { get; set; }
        public Employee Employee { get; set; }
    }
}
