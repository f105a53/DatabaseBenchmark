using System;
using System.Collections.Generic;

namespace DatabaseBenchmark.Model
{
    public partial class Dependent
    {
        public decimal Essn { get; set; }
        public string DependentName { get; set; }
        public string Sex { get; set; }
        public DateTime? Bdate { get; set; }
        public string Relationship { get; set; }

        public virtual Employee EssnNavigation { get; set; }
    }
}
