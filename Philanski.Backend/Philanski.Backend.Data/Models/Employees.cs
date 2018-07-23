using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Managers = new HashSet<Managers>();
            TimeSheetApprovals = new HashSet<TimeSheetApprovals>();
            TimeSheets = new HashSet<TimeSheets>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public int WorksiteId { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public Worksites Worksite { get; set; }
        public ICollection<Managers> Managers { get; set; }
        public ICollection<TimeSheetApprovals> TimeSheetApprovals { get; set; }
        public ICollection<TimeSheets> TimeSheets { get; set; }
    }
}
