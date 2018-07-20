using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data
{
    public partial class Worksite
    {
        public Worksite()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}
