using System;
using System.Collections.Generic;
using System.Text;

namespace Philanski.Frontend.Library.Models
{
    public class Worksite
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
