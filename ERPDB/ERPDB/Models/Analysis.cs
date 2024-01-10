using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Analysis
    {
        public Analysis()
        {
            RAnalysis = new HashSet<RAnalysis>();
        }

        public int AnalysisId { get; set; }
        public string APric { get; set; }
        public string AName { get; set; }
        public int Stats { get; set; }

        public ICollection<RAnalysis> RAnalysis { get; set; }
    }
}
