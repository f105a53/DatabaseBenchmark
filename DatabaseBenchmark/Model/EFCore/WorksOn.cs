namespace DatabaseBenchmark.Model.EFCore
{
    public partial class WorksOn
    {
        public decimal Essn { get; set; }
        public int Pno { get; set; }
        public double? Hours { get; set; }

        public virtual Linq2Db.Employee EssnNavigation { get; set; }
        public virtual Linq2Db.Project PnoNavigation { get; set; }
    }
}
