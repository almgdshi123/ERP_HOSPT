using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Interview
    {
        public Interview()
        {
            Diagnosis = new HashSet<Diagnosis>();
            RAnalysis = new HashSet<RAnalysis>();
        }

        public int InterviewId { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public string InterDate { get; set; }
        public string InterNotes { get; set; }
        public string InterType { get; set; }
        public string UserId { get; set; }
        public bool State { get; set; }

        public Patient Patient { get; set; }
        public Physician Physician { get; set; }
        public AspNetUsers User { get; set; }
        public ICollection<Diagnosis> Diagnosis { get; set; }
        public ICollection<RAnalysis> RAnalysis { get; set; }
    }
}
