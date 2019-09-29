using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using ToDataTable.TestDataGenerator;

namespace ToDataTable.Benchmarks
{
    public class BenchmarkConstructorCache
    {
        private IEnumerable<TestClass> _data;
        private IEnumerable<PrecompiledTestClass> _precompiledData;

        [Benchmark]
        public void Version1()
        {
            for (var i = 0; i < 1000; i++)
            {
                _data.ToDataTableVersion1();
            }
        }

        [Benchmark]
        public void CurrentVersion()
        {
            for (var i = 0; i < 1000; i++)
            {
                _data.ToDataTable();
            }
        }

        [Benchmark]
        public void CurrentVersionPrecompiled()
        {
            for (var i = 0; i < 1000; i++)
            {
                _precompiledData.ToDataTable();
            }
        }

        [GlobalSetup]
        public void Setup()
        {
            ToDataTableContext.PrecompileMaps(typeof(PrecompiledTestClass).Assembly);
            _data = Generator.GetTestEnumerable(10);
            _precompiledData = Generator.GetPrecompiledTestEnumerable(10);
        }
    }
}