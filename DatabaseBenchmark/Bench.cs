using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using DatabaseBenchmark.Model.EFCore;
using DatabaseBenchmark.Model.Linq2Db;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Employee = DatabaseBenchmark.Model.EFCore.Employee;

namespace DatabaseBenchmark
{
    [CoreJob]
    [MarkdownExporter]
    [RPlotExporter]
    [MemoryDiagnoser]
    [AsciiDocExporter]
    [ReturnValueValidator]
    [ExecutionValidator]
    public class Bench
    {
        public enum Workload
        {
            SelectEmployees,
            SelectEmployeeCount,
            UpdateEmployee,
            CreateDelete
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

            int CreateDelete()
            {
                var id = 11;
                var e = new Employee
                {
                    Ssn = id
                };
                var entry = EfCoreContext.Entry(e);
                entry.State = EntityState.Added;
                EfCoreContext.SaveChanges();
                entry.State = EntityState.Deleted;
                EfCoreContext.SaveChanges();
                return EfCoreContext.Employee.Count(em => em.Ssn == id);
            }

            return W switch
                {
                Workload.SelectEmployees => EfCoreContext.Employee.ToList().Count.ToString(),
                Workload.SelectEmployeeCount => EfCoreContext.Employee.Count().ToString(),
                Workload.UpdateEmployee => UpdateEmployee().ToString(),
                Workload.CreateDelete => CreateDelete().ToString()
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

            int CreateDelete()
            {
                var id = 13;
                var e = new Model.Linq2Db.Employee
                {
                    SSN = id
                };
                Linq2DbContext.Insert(e).MustBeOne();
                Linq2DbContext.Delete(e).MustBeOne();
                return Linq2DbContext.Employees.Count(em => em.SSN == id);
            }

            return W switch
                {
                Workload.SelectEmployees => Linq2DbContext.Employees.ToList().Count.ToString(),
                Workload.SelectEmployeeCount => Linq2DbContext.Employees.Count().ToString(),
                Workload.UpdateEmployee => UpdateEmployee().ToString(),
                Workload.CreateDelete => CreateDelete().ToString()
                };
        }

        [Benchmark]
        public string SQL()
        {
            int CountRows()
            {
                using var command = new SqlCommand("SELECT * FROM [Company].[dbo].[Employee]", sqlConnection);
                using var reader = command.ExecuteReader();
                var i = 0;
                while (reader.Read()) i++;
                reader.Close();
                return i;
            }

            int CreateDelete()
            {
                var id = 14;
                using var add = new SqlCommand($"INSERT INTO Employee(SSN) VALUES({id})", sqlConnection);
                    add.ExecuteNonQuery().MustBeOne();
                    using var del = new SqlCommand($"DELETE FROM Employee WHERE SSN={id}", sqlConnection);
                    del.ExecuteNonQuery().MustBeOne();
                    using var check = new SqlCommand($"SELECT COUNT(SSN) FROM Employee WHERE SSN={id}", sqlConnection);
                return (int)check.ExecuteScalar();
            }

            return W switch
                {
                Workload.SelectEmployees => CountRows()
                    .ToString(),
                Workload.SelectEmployeeCount =>
                ((int) new SqlCommand("SELECT COUNT(SSN) FROM Employee", sqlConnection).ExecuteScalar()).ToString(),
                Workload.UpdateEmployee => new SqlCommand(
                        $"UPDATE Employee SET Address='{DateTimeOffset.Now.ToString()}' WHERE SSN='123456789'",
                        sqlConnection)
                    .ExecuteNonQuery().ToString(),
                Workload.CreateDelete => CreateDelete().ToString()
                };
        }
    }
}