using BenchmarkDotNet.Running;
using System;

namespace ToDataTable.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkToDataTableVersions>();
            Console.ReadLine();
        }
    }
}