using System;

namespace DatabaseBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<SelectJoinBenchmark>();
        }
    }
}
