using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class City
    {
        public City()
        {
            Patient = new HashSet<Patient>();
            Physician = new HashSet<Physician>();
            Region = new HashSet<Region>();
        }

        public int CityId { get; set; }
        public string CitName { get; set; }

        public ICollection<Patient> Patient { get; set; }
        public ICollection<Physician> Physician { get; set; }
        public ICollection<Region> Region { get; set; }
    }
}
