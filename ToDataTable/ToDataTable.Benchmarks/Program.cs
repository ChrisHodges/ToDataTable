using BenchmarkDotNet.Running;

namespace ToDataTable.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkToDataTableVersions>();
            BenchmarkRunner.Run<BenchmarkConstructorCache>();
        }
    }
}