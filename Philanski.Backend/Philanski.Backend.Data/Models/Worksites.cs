using System;
using System.Collections.Generic;

namespace Philanski.Backend.Data
{
    public partial class Worksites
    {
        public Worksites()
        {
            Employees = new HashSet<Employees>();
        }

        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public ICollection<Employees> Employees { get; set; }
    }
}
