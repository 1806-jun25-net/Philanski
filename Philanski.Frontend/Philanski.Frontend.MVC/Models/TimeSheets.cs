using System;
using System.Collections.Generic;
using System.Text;

namespace Philanski.Frontend.MVC.Models
{
    public class TimeSheets
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public decimal RegularHours { get; set; }
    }
}
