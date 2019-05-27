using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Model.EFCore;
using DatabaseBenchmark.Model.Linq2Db;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Employee = DatabaseBenchmark.Model.EFCore.Employee;

namespace DatabaseBenchmark
{
    [CoreJob]
    [MarkdownExporter]
    [HtmlExporter]
    [RPlotExporter]
    [RankColumn]
    [MemoryDiagnoser]
    [ReturnValueValidator]
    public class SelectJoinBenchmark
    {
        private readonly CompanyContext EfCoreContext = new CompanyContext();

        private readonly CompanyDB Linq2DbContext;

        public SelectJoinBenchmark()
        {
            DataConnection.AddConfiguration("Default", "Server=localhost;Database=Company;Trusted_Connection=True;",
                new SqlServerDataProvider("Default", SqlServerVersion.v2017));
            Linq2DbContext = new CompanyDB("Default");
        }

        [Benchmark]
        public async Task<int> EFCore()
        {
            return await EfCoreContext.Employee.SumAsyncEF(employee => employee.WorksOn.Count);
        }

        [Benchmark]
        public async Task<int> Linq2Db()
        {
            return await Linq2DbContext.Employees.SumAsync(employee => employee.Worksons.Count());
        }
    }
}