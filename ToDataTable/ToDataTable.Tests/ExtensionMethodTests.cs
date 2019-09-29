using System;
using System.Linq;
using FluentAssertions;
using ToDataTable.TestDataGenerator;
using Xunit;

namespace ToDataTable.Tests
{
    public class ExtensionMethodTests
    {
        [Fact]
        public void ColumnCountIsCorrect()
        {
            var dataTable = Generator.GetTestEnumerable().ToDataTable();
            dataTable.Columns.Count.Should().Be(14);
        }

        [Fact]
        public void ColumnNamesAreCorrect()
        {
            var dataTable = Generator.GetTestEnumerable().ToDataTable();
            dataTable.Columns[0].ColumnName.Should().Be("Int");
        }

        [Fact]
        private void HandlesBinary()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[7].Should().Be(testEnumerable.First().Binary);
        }

        [Fact]
        private void HandlesBoolean()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[8].Should().Be(testEnumerable.First().Boolean);
        }

        [Fact]
        private void HandlesDateTime()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[5].Should().Be(testEnumerable.First().DateTime);
        }

        [Fact]
        private void HandlesDecimal()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[11].Should().Be(testEnumerable.First().Decimal);
        }

        [Fact]
        private void HandlesDouble()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[13].Should().Be(testEnumerable.First().Double);
        }

        [Fact]
        private void HandlesGuid()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[3].Should().Be(testEnumerable.First().Guid);
        }

        [Fact]
        public void HandlesInt()
        {
            var dataTable = Generator.GetTestEnumerable().ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[0].Should().Be(1);
        }

        [Fact]
        private void HandlesInt16()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[9].Should().Be(testEnumerable.First().Int16);
        }

        [Fact]
        private void HandlesInt64()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[10].Should().Be(testEnumerable.First().Int64);
        }

        [Fact]
        private void HandlesNullableDateTime()
        {
            var dataTable = Generator.GetTestEnumerable().ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[6].Should().Be(DBNull.Value);
        }

        [Fact]
        private void HandlesNullableGuid()
        {
            var dataTable = Generator.GetTestEnumerable().ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[4].Should().Be(DBNull.Value);
        }

        [Fact]
        private void HandlesNullableInt()
        {
            var dataTable = Generator.GetTestEnumerable().ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[1].Should().Be(DBNull.Value);
        }

        [Fact]
        private void HandlesSingle()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[12].Should().Be(testEnumerable.First().Single);
        }

        [Fact]
        private void HandlesString()
        {
            var testEnumerable = Generator.GetTestEnumerable();
            var dataTable = testEnumerable.ToDataTable();
            var dataRow = dataTable.Rows[0];
            dataRow[2].Should().Be(testEnumerable.First().String);
        }

        [Fact]
        public void HasCorrectNumberOfRows()
        {
            var dataTable = Generator.GetTestEnumerable().ToDataTable();
            dataTable.Rows.Count.Should().Be(2);
        }
    }
}