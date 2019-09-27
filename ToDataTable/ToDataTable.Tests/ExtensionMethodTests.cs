using System;
using System.Data;
using System.Linq;
using FluentAssertions;
using ToDataTable.TestDataGenerator;
using Xunit;

namespace ToDataTable.Tests
{
    public class ExtensionMethodTests
    {
        [Fact]
        public void HasCorrectNumberOfRows()
        {
            
            DataTable dataTable = Generator.GetTestEnumerable().ToDataTable();
            dataTable.Rows.Count.Should().Be(2);
        }

        [Fact]
        public void ColumnCountIsCorrect()
        {
            DataTable dataTable = Generator.GetTestEnumerable().ToDataTable();
            dataTable.Columns.Count.Should().Be(14);
        }

        [Fact]
        public void ColumnNamesAreCorrect()
        {
            DataTable dataTable = Generator.GetTestEnumerable().ToDataTable();
            dataTable.Columns[0].ColumnName.Should().Be("Int");
        }

        [Fact]
        public void HandlesInt()
        {
            DataTable dataTable = Generator.GetTestEnumerable().ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[0].Should().Be(1);
        }
        
        [Fact]
        void HandlesNullableInt()
        {
            DataTable dataTable = Generator.GetTestEnumerable().ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[1].Should().Be(DBNull.Value);
        }

        [Fact]
        void HandlesString()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[2].Should().Be(testEnumerable.First().String);
        }
        
        [Fact]
        void HandlesGuid()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[3].Should().Be(testEnumerable.First().Guid);
        }
        
        [Fact]
        void HandlesNullableGuid()
        {
            DataTable dataTable = Generator.GetTestEnumerable().ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[4].Should().Be(DBNull.Value);
        }
        
        [Fact]
        void HandlesDateTime()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[5].Should().Be(testEnumerable.First().DateTime);
        }
        
        [Fact]
        void HandlesNullableDateTime()
        {
            DataTable dataTable = Generator.GetTestEnumerable().ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[6].Should().Be(DBNull.Value);
        }
        
        [Fact]
        void HandlesBinary()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[7].Should().Be(testEnumerable.First().Binary);
        }
        
        [Fact]
        void HandlesBoolean()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[8].Should().Be(testEnumerable.First().Boolean);
        }
        
        [Fact]
        void HandlesInt16()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[9].Should().Be(testEnumerable.First().Int16);
        }
        
        [Fact]
        void HandlesInt64()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[10].Should().Be(testEnumerable.First().Int64);
        }
        
        [Fact]
        void HandlesDecimal()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[11].Should().Be(testEnumerable.First().Decimal);
        }
        
        [Fact]
        void HandlesSingle()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[12].Should().Be(testEnumerable.First().Single);
        }
        
        [Fact]
        void HandlesDouble()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            DataTable dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[13].Should().Be(testEnumerable.First().Double);
        }
    }
}