using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Physician
    {
        public Physician()
        {
            Diagnosis = new HashSet<Diagnosis>();
            Interview = new HashSet<Interview>();
            Prescribtion = new HashSet<Prescribtion>();
            RAnalysis = new HashSet<RAnalysis>();
        }

        public int PhysicianId { get; set; }
        public int Departno { get; set; }
        public int QualifyId { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; }
        public string PhyAddr { get; set; }
        public string PhyBirth { get; set; }
        public string PhyEmil { get; set; }
        public string PhyName { get; set; }
        public string PhyPhone { get; set; }
        public string PhySex { get; set; }
        public string UserId { get; set; }

        public City City { get; set; }
        public Depart DepartnoNavigation { get; set; }
        public Qualify Qualify { get; set; }
        public Region Region { get; set; }
        public AspNetUsers User { get; set; }
        public ICollection<Diagnosis> Diagnosis { get; set; }
        public ICollection<Interview> Interview { get; set; }
        public ICollection<Prescribtion> Prescribtion { get; set; }
        public ICollection<RAnalysis> RAnalysis { get; set; }
    }
}
