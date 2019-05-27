using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Model.EFCore;
using DatabaseBenchmark.Model.Linq2Db;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;

namespace DatabaseBenchmark
{
    [CoreJob]
    [MarkdownExporter]
    [RPlotExporter]
    [MemoryDiagnoser]
    [ReturnValueValidator(true)]
    public class SelectJoinBenchmark
    {
        public enum Workload
        {
            SelectEmployees,
            SelectEmployeeCount,
            UpdateEmployee
        }

        private readonly CompanyContext EfCoreContext = new CompanyContext();

        private readonly CompanyDB Linq2DbContext;

        public SelectJoinBenchmark()
        {
            DataConnection.AddConfiguration("Default", "Server=localhost;Database=Company;Trusted_Connection=True;",
                new SqlServerDataProvider("Default", SqlServerVersion.v2017));
            Linq2DbContext = new CompanyDB("Default");
        }

        [ParamsAllValues] public Workload SelectedWorkload { get; set; }

        [Benchmark(Baseline = true)]
        public ICollection<int> EFCore()
        {
            int UpdateEmployee()
            {
                var employee = EfCoreContext.Employee.Find(123456789m);
                employee.Address = DateTimeOffset.Now.ToString();
                EfCoreContext.Update(employee);
                EfCoreContext.SaveChanges();
                return 1;
            }

            return SelectedWorkload switch
                {
                Workload.SelectEmployees => EfCoreContext.Employee.ToList().Count.ToListSignleItem(),
                Workload.SelectEmployeeCount => EfCoreContext.Employee.Count().ToListSignleItem(),
                Workload.UpdateEmployee => UpdateEmployee().ToListSignleItem(),
                };
        }

        [Benchmark]
        public ICollection<int> Linq2Db()
        {
            int UpdateEmployee()
            {
                var employee = Linq2DbContext.Employees.Find(123456789);
                employee.Address = DateTimeOffset.Now.ToString();
                Linq2DbContext.Update(employee);
                return 1;
            }

            return SelectedWorkload switch
                {
                Workload.SelectEmployees => Linq2DbContext.Employees.ToList().Count.ToListSignleItem(),
                Workload.SelectEmployeeCount => Linq2DbContext.Employees.Count().ToListSignleItem(),
                Workload.UpdateEmployee => UpdateEmployee().ToListSignleItem(),
                };
        }
    }
}