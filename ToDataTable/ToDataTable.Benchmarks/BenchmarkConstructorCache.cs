using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using ToDataTable.TestDataGenerator;

namespace ToDataTable.Benchmarks
{
    public class BenchmarkConstructorCache
    {
        private IEnumerable<TestClass> _data;

        [Benchmark]
        public void Version1()
        {
            for (var i = 0; i < 10000; i++)
            {
                _data.ToDataTableVersion1();
            }
        }
        
        [Benchmark]
        public void CurrentVersion()
        {
            for (var i = 0; i < 10000; i++)
            {
                _data.ToDataTable();
            }
        }

        [GlobalSetup]
        public void Setup()
        {
            _data = Generator.GetTestEnumerable(10);
        }
    }
}