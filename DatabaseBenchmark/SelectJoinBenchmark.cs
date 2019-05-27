using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace DatabaseBenchmark
{
    [CoreJob, MarkdownExporter, HtmlExporter, RPlotExporter, RankColumn, MemoryDiagnoser]
    public class SelectJoinBenchmark
    {
        public SelectJoinBenchmark()
        {
            
        }

        //private readonly CompanyContext EfCoreContext = new CompanyContext();
       // private readonly CompanyDB Linq2DbContext = new CompanyDB("Server=localhost;Database=Company;Trusted_Connection=True;");

        //[Benchmark]
        //public async Task<List<Employee>> EFCore()
        //{
        //    return return await EfCoreContext.Employee.Include(e=>e).ThenInclude(w=>w.PnoNavigation).ToListAsync();
        //}
        //[Benchmark]
        //public async Task<List<Employee>> Linq2Db()
        //{
        //    return
        //        null await Linq2DbContext.Employees.Include(e => e.Worksons).ThenInclude(w => w.WorksonProject).ToListAsync();
        //}

    }

}
