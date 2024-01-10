using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_HOSPT.modelcla
{
    public class Interview_and_anlasis
    {
      
        public int interviewId { get; set; }
    
        public string inter_type { get; set; }

       
        public string inter_date { get; set; }
      
        public string inter_notes { get; set; }
      
        public string userId { get; set; }
      
        public int PatientId { get; set; }
      
        public int PhysicianId { get; set; }

        public int AnalysisId { get; set; }
       
        public string a_name { get; set; }
       
        public string a_Pric { get; set; }
    }
}
