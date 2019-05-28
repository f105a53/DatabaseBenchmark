using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Model.EFCore;
using DatabaseBenchmark.Model.Linq2Db;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.Data.SqlClient;

namespace DatabaseBenchmark
{
    [CoreJob]
    [MarkdownExporter]
    [RPlotExporter]
    [MemoryDiagnoser]
    [ReturnValueValidator(true)]
    public class Bench
    {
     
        public enum Workload
        {
            SelectEmployees,
            SelectEmployeeCount,
            UpdateEmployee
        }

        private readonly CompanyContext EfCoreContext = new CompanyContext();

        private readonly CompanyDB Linq2DbContext;

        private readonly SqlConnection sqlConnection =
            new SqlConnection("Server=localhost;Database=Company;Trusted_Connection=True;");

        public Bench()
        {
            DataConnection.AddConfiguration("Default", "Server=localhost;Database=Company;Trusted_Connection=True;",
                new SqlServerDataProvider("Default", SqlServerVersion.v2017));
            Linq2DbContext = new CompanyDB("Default");
            sqlConnection.Open();
        }

        [ParamsAllValues] public Workload W { get; set; }

        [Benchmark(Baseline = true)]
        public string EFCore()
        {
            int UpdateEmployee()
            {
                var employee = EfCoreContext.Employee.Find(123456789m);
                employee.Address = DateTimeOffset.Now.ToString();
                EfCoreContext.Update(employee);
                EfCoreContext.SaveChanges();
                return 1;
            }

            return W switch
                {
                Workload.SelectEmployees => EfCoreContext.Employee.ToList().Count.ToString(),
                Workload.SelectEmployeeCount => EfCoreContext.Employee.Count().ToString(),
                Workload.UpdateEmployee => UpdateEmployee().ToString(),
                };
        }

        [Benchmark]
        public string Linq2Db()
        {
            int UpdateEmployee()
            {
                var employee = Linq2DbContext.Employees.Find(123456789);
                employee.Address = DateTimeOffset.Now.ToString();
                Linq2DbContext.Update(employee);
                return 1;
            }

            return W switch
                {
                Workload.SelectEmployees => Linq2DbContext.Employees.ToList().Count.ToString(),
                Workload.SelectEmployeeCount => Linq2DbContext.Employees.Count().ToString(),
                Workload.UpdateEmployee => UpdateEmployee().ToString(),
                };
        }

        [Benchmark]
        public string SQL()
        {
            int CountRows( )
            {
                using var command = new SqlCommand("SELECT * FROM [Company].[dbo].[Employee]", sqlConnection);
                using var reader = command.ExecuteReader();
                var i = 0;
                while (reader.Read()) i++;
                reader.Close();
                return i;
            }

            return W switch
                {
                Workload.SelectEmployees => CountRows()
                    .ToString(),
                Workload.SelectEmployeeCount =>
                ((int) new SqlCommand("SELECT COUNT(SSN) FROM Employee", sqlConnection).ExecuteScalar()).ToString(),
                Workload.UpdateEmployee => new SqlCommand(
                        $"UPDATE Employee SET Address='{DateTimeOffset.Now.ToString()}' WHERE SSN='123456789'", sqlConnection)
                    .ExecuteNonQuery().ToString(),
                };
        }
    }
}