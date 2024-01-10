using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Region
    {
        public Region()
        {
            Patient = new HashSet<Patient>();
            Physician = new HashSet<Physician>();
        }

        public int RegionId { get; set; }
        public int CityId { get; set; }
        public string RegName { get; set; }

        public City City { get; set; }
        public ICollection<Patient> Patient { get; set; }
        public ICollection<Physician> Physician { get; set; }
    }
}
