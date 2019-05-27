using System;

namespace DatabaseBenchmark.Model.EFCore
{
    public partial class Dependent
    {
        public decimal Essn { get; set; }
        public string DependentName { get; set; }
        public string Sex { get; set; }
        public DateTime? Bdate { get; set; }
        public string Relationship { get; set; }

        public virtual Linq2Db.Employee EssnNavigation { get; set; }
    }
}
