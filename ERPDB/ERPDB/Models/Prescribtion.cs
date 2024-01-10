using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Prescribtion
    {
        public int PrescribtionId { get; set; }
        public string Dig { get; set; }
        public int DrugId { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public string PreDate { get; set; }
        public string PreDetail { get; set; }

        public Drug Drug { get; set; }
        public Patient Patient { get; set; }
        public Physician Physician { get; set; }
    }
}
