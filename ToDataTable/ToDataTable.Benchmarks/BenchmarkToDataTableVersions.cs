using System.Collections.Generic;
using System.Data;
using BenchmarkDotNet.Attributes;
using ToDataTable.TestDataGenerator;

namespace ToDataTable.Benchmarks
{
    public class BenchmarkToDataTableVersions
    {
        private IEnumerable<TestClass> _data;

        [Benchmark]
        public DataTable Version1()
        {
            return _data.ToDataTableVersion1();
        }
        
        [Benchmark]
        public DataTable CurrentVersion()
        {
            return _data.ToDataTable();
        }

        [GlobalSetup]
        public void Setup()
        {
            _data = Generator.GetTestEnumerable(100000);
        }
    }
}