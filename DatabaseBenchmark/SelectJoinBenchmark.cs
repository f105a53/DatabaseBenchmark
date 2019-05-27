using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Model.EFCore;
using DatabaseBenchmark.Model.Linq2Db;
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
    public class SelectJoinBenchmark
    {
        private readonly CompanyContext EfCoreContext = new CompanyContext();

        private readonly CompanyDB Linq2DbContext =
            new CompanyDB("Server=localhost;Database=Company;Trusted_Connection=True;");

        [Benchmark]
        public async Task<List<Employee>> EFCore()
        {
            return await EfCoreContext.Employee.Include(e => e.WorksOn).ThenInclude(w => w.PnoNavigation).ToListAsync();
        }

        [Benchmark]
        public async Task<List<Model.Linq2Db.Employee>> Linq2Db()
        {
            return await Linq2DbContext.Employees.Include(e => e.Worksons).ThenInclude(w => w.WorksonProject)
                .ToListAsync();
        }
    }
}