using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Qualify
    {
        public Qualify()
        {
            Physician = new HashSet<Physician>();
        }

        public int QualifyId { get; set; }
        public string QName { get; set; }

        public ICollection<Physician> Physician { get; set; }
    }
}
