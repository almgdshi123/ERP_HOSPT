using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Diagnosis = new HashSet<Diagnosis>();
            Interview = new HashSet<Interview>();
            Prescribtion = new HashSet<Prescribtion>();
            RAnalysis = new HashSet<RAnalysis>();
        }

        public int PatientId { get; set; }
        public string Patientname { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; }
        public string PaAddr { get; set; }
        public string PaData { get; set; }
        public string PaEmail { get; set; }
        public string PaJob { get; set; }
        public string PaMobile { get; set; }
        public string PaNat { get; set; }
        public string PaNote { get; set; }
        public string PaPhone { get; set; }
        public string PaSex { get; set; }

        public City City { get; set; }
        public Region Region { get; set; }
        public ICollection<Diagnosis> Diagnosis { get; set; }
        public ICollection<Interview> Interview { get; set; }
        public ICollection<Prescribtion> Prescribtion { get; set; }
        public ICollection<RAnalysis> RAnalysis { get; set; }
    }
}
