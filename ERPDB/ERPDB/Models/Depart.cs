using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Depart
    {
        public Depart()
        {
            Physician = new HashSet<Physician>();
        }

        public int Departno { get; set; }
        public string DeptName { get; set; }

        public ICollection<Physician> Physician { get; set; }
    }
}
