using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data
{
    public partial class TimeSheet
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public decimal RegularHours { get; set; }

        public Employee Employee { get; set; }
    }
}
