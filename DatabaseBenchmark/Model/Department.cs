using System;
using System.Collections.Generic;

namespace DatabaseBenchmark.Model
{
    public partial class Department
    {
        public Department()
        {
            DeptLocations = new HashSet<DeptLocations>();
            Employee = new HashSet<Employee>();
            Project = new HashSet<Project>();
        }

        public string Dname { get; set; }
        public int Dnumber { get; set; }
        public decimal? MgrSsn { get; set; }
        public DateTime? MgrStartDate { get; set; }

        public virtual Employee MgrSsnNavigation { get; set; }
        public virtual ICollection<DeptLocations> DeptLocations { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Project> Project { get; set; }
    }
}
