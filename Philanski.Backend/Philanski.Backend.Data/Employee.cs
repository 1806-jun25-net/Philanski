using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data
{
    public partial class Employee
    {
        public Employee()
        {
            Manager = new HashSet<Manager>();
            TimeSheet = new HashSet<TimeSheet>();
            TimeSheetApproval = new HashSet<TimeSheetApproval>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public int WorksiteId { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public Worksite Worksite { get; set; }
        public ICollection<Manager> Manager { get; set; }
        public ICollection<TimeSheet> TimeSheet { get; set; }
        public ICollection<TimeSheetApproval> TimeSheetApproval { get; set; }
    }
}
