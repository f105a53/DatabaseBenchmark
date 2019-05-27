using System;
using System.Collections.Generic;

namespace DatabaseBenchmark.Model.EFCore
{
    public partial class Department
    {
        public Department()
        {
            DeptLocations = new HashSet<DeptLocations>();
            Employee = new HashSet<Linq2Db.Employee>();
            Project = new HashSet<Linq2Db.Project>();
        }

        public string Dname { get; set; }
        public int Dnumber { get; set; }
        public decimal? MgrSsn { get; set; }
        public DateTime? MgrStartDate { get; set; }

        public virtual Linq2Db.Employee MgrSsnNavigation { get; set; }
        public virtual ICollection<DeptLocations> DeptLocations { get; set; }
        public virtual ICollection<Linq2Db.Employee> Employee { get; set; }
        public virtual ICollection<Linq2Db.Project> Project { get; set; }
    }
}
