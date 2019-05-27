using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Model.EFCore;
using DatabaseBenchmark.Model.Linq2Db;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;

namespace DatabaseBenchmark
{
    [CoreJob]
    [MarkdownExporter]
    [RPlotExporter]
    [MemoryDiagnoser]
    [ReturnValueValidator(false)]
    public class SelectJoinBenchmark
    {
        public enum Workload
        {
            SelectEmployees,
            SelectEmployeeCount
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

        [Benchmark]
        public ICollection<int> EFCore()
        {
            return SelectedWorkload switch
                {
                Workload.SelectEmployees => EfCoreContext.Employee.ToListSignleItem().Count.ToListSignleItem(),
                Workload.SelectEmployeeCount => EfCoreContext.Employee.Count().ToListSignleItem()
                };
        }

        [Benchmark]
        public ICollection<int> Linq2Db()
        {
            return SelectedWorkload switch
                {
                Workload.SelectEmployees => Linq2DbContext.Employees.ToList().Count.ToListSignleItem(),
                Workload.SelectEmployeeCount => Linq2DbContext.Employees.Count().ToListSignleItem()
                };
        }
    }
}