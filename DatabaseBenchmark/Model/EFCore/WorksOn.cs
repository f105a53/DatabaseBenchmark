using System;
using System.Collections.Generic;

namespace DatabaseBenchmark.Model.EFCore
{
    public partial class WorksOn
    {
        public decimal Essn { get; set; }
        public int Pno { get; set; }
        public double? Hours { get; set; }

        public virtual Employee EssnNavigation { get; set; }
        public virtual Project PnoNavigation { get; set; }
    }
}
