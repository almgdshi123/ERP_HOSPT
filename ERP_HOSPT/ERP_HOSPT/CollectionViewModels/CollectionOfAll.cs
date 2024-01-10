using ERP_HOSPT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_HOSPT.CollectionViewModels
{
    public class CollectionOfAll
    {
        public IEnumerable<interview> interviewss { get; set; }
        public IEnumerable<Depart> Departss { get; set; }
        public IEnumerable<Patient> Patientss { get; set; }
        public IEnumerable<Physician> Physicianss { get; set; }
        public IEnumerable<City> Cityss { get; set; }
        public IEnumerable<Region> Regionss { get; set; }


    }
}
