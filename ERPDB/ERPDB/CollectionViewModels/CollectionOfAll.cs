using ERPDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPDB.CollectionViewModels
{
    public class CollectionOfAll
    {
        public IEnumerable<Interview> interviewss { get; set; }
        public IEnumerable<Depart> Departss { get; set; }
        public IEnumerable<Patient> Patientss { get; set; }
        public IEnumerable<Physician> Physicianss { get; set; }
        public IEnumerable<City> Cityss { get; set; }
        public IEnumerable<Region> Regionss { get; set; }


    }
}
