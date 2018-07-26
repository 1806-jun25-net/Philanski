using System;
using System.Collections.Generic;
using System.Text;

namespace Philanski.Frontend.Library.Models
{
    public class TimeSheet
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public decimal RegularHours { get; set; }
    }
}
