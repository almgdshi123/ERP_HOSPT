using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Diagnosis
    {
        public int DiagnosisId { get; set; }
        public string DiagnosisDate { get; set; }
        public string Dig { get; set; }
        public string Drug { get; set; }
        public string DrugDetail { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public int InterviewId { get; set; }

        public Interview Interview { get; set; }
        public Patient Patient { get; set; }
        public Physician Physician { get; set; }
    }
}
