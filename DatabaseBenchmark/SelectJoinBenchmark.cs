using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Model;
using DatabaseBenchmark.Model.EFCore;
using DatabaseBenchmark.Model.Linq2Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Employee = DatabaseBenchmark.Model.Linq2Db.Employee;

namespace DatabaseBenchmark
{
    [CoreJob, MarkdownExporter, HtmlExporter, RPlotExporter, RankColumn, MemoryDiagnoser]
    public class SelectJoinBenchmark
    {
        public SelectJoinBenchmark()
        {
            
        }

        private readonly CompanyContext EfCoreContext = new CompanyContext();
        private readonly CompanyDb Linq2DbContext = new Model.Linq2Db.CompanyDb();

        [Benchmark]
        public async Task<List<Employee>> EFCore()
        {
            return await EfCoreContext.Employee.Include(e=>e.WorksOn).ThenInclude(w=>w.PnoNavigation).ToListAsync();
        }

        public async Task<List<Employee>> Linq2Db()
        {
            return null;
        }

    }

}
