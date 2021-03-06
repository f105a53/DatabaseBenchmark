﻿using System;
using System.Collections.Generic;

namespace DatabaseBenchmark.Model.EFCore
{
    public partial class Employee
    {
        public Employee()
        {
            Department = new HashSet<Department>();
            Dependent = new HashSet<Dependent>();
            InverseSuperSsnNavigation = new HashSet<Employee>();
            WorksOn = new HashSet<WorksOn>();
        }

        public string Fname { get; set; }
        public string Minit { get; set; }
        public string Lname { get; set; }
        public decimal Ssn { get; set; }
        public DateTime? Bdate { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public decimal? Salary { get; set; }
        public decimal? SuperSsn { get; set; }
        public int? Dno { get; set; }

        public virtual Department DnoNavigation { get; set; }
        public virtual Employee SuperSsnNavigation { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<Dependent> Dependent { get; set; }
        public virtual ICollection<Employee> InverseSuperSsnNavigation { get; set; }
        public virtual ICollection<WorksOn> WorksOn { get; set; }
    }
}
