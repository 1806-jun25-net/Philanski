using System;
using System.Collections.Generic;

namespace Philanski.Backend.DataContext.Models
{
    public partial class TimeSheets
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public decimal RegularHours { get; set; }

        public Employees Employee { get; set; }
    }
}
