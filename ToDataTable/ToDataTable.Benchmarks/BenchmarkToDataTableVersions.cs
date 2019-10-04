using System.Collections.Generic;
using System.Data;
using BenchmarkDotNet.Attributes;
using ToDataTable.TestDataGenerator;

namespace ToDataTable.Benchmarks
{
    public class BenchmarkToDataTableVersions
    {
        private IEnumerable<TestClass> _data;
        private IEnumerable<PrecompiledTestClass> _precompiledHundredRows;
        private IEnumerable<TestClass> _hundredRows;

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

        [Benchmark]
        public DataTable UsingFastMember()
        {
            return _data.FastMemberToDataTable();
        }

        [Benchmark]
        public DataTable UsingFastMemberAnswerFromStackOverflow564366()
        {
            return _data.FastMemberFromStackOverflow564366ToDatatable();
        }
        
        [Benchmark]
        public List<DataTable>  Version1Create100Tables()
        {
            var outputList = new List<DataTable>(); 
            for (var i = 0; i < 1000; i++)
            {
                outputList.Add(_hundredRows.ToDataTableVersion1());
            }
            return outputList;
        }

        [Benchmark]
        public List<DataTable>  CurrentVersionCreate100Tables()
        {
            var outputList = new List<DataTable>(); 
            for (var i = 0; i < 1000; i++)
            {
                outputList.Add(_hundredRows.ToDataTable());
            }
            return outputList;
        }

        [Benchmark]
        public List<DataTable>  CurrentVersionPrecompiledCreate100Tables()
        {
            var outputList = new List<DataTable>(); 
            for (var i = 0; i < 1000; i++)
            {
                outputList.Add(_precompiledHundredRows.ToDataTable());
            }
            return outputList;
        }

        [Benchmark]
        public List<DataTable> UsingFastMemberCreate100Tables()
        {
            var outputList = new List<DataTable>(); 
            for (var i = 0; i < 1000; i++)
            {
                outputList.Add(_hundredRows.FastMemberToDataTable());
            }
            return outputList;
        }

        [GlobalSetup]
        public void Setup()
        {
            _data = Generator.GetTestEnumerable(100000);
            _hundredRows = Generator.GetTestEnumerable(100);
            _precompiledHundredRows = Generator.GetPrecompiledTestEnumerable(100);
        }
    }
}