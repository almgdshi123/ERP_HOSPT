using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class RAnalysis
    {
        public int RAnalysisId { get; set; }
        public int AnalysisId { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public int InterviewId { get; set; }
        public string RDate { get; set; }
        public string RDescribe { get; set; }
        public string RResult { get; set; }
        public bool Trboll { get; set; }
        public bool? State1 { get; set; }

        public Analysis Analysis { get; set; }
        public Interview Interview { get; set; }
        public Patient Patient { get; set; }
        public Physician Physician { get; set; }
    }
}
