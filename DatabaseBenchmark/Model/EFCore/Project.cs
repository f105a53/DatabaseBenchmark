using System.Collections.Generic;

namespace DatabaseBenchmark.Model.EFCore
{
    public partial class Project
    {
        public Project()
        {
            WorksOn = new HashSet<Linq2Db.WorksOn>();
        }

        public string Pname { get; set; }
        public int Pnumber { get; set; }
        public string Plocation { get; set; }
        public int? Dnum { get; set; }

        public virtual Department DnumNavigation { get; set; }
        public virtual ICollection<Linq2Db.WorksOn> WorksOn { get; set; }
    }
}
