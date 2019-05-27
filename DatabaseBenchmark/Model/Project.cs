using System;
using System.Collections.Generic;

namespace DatabaseBenchmark.Model
{
    public partial class Project
    {
        public Project()
        {
            WorksOn = new HashSet<WorksOn>();
        }

        public string Pname { get; set; }
        public int Pnumber { get; set; }
        public string Plocation { get; set; }
        public int? Dnum { get; set; }

        public virtual Department DnumNavigation { get; set; }
        public virtual ICollection<WorksOn> WorksOn { get; set; }
    }
}
