using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Drug
    {
        public Drug()
        {
            Prescribtion = new HashSet<Prescribtion>();
        }

        public int DrugId { get; set; }
        public string DName { get; set; }
        public string DPric { get; set; }

        public ICollection<Prescribtion> Prescribtion { get; set; }
    }
}
