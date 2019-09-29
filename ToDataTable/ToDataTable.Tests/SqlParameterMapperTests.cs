using System.Data;
using FluentAssertions;
using ToDataTable.TestDataGenerator;
using Xunit;

namespace ToDataTable.Tests
{
    public class SqlParameterMapperTests
    {
        [Fact]
        public void DbTypeIsCorrect()
        {
            var sqlParameter = Generator.GetTestEnumerable().ToSqlParameter("TestName", "typeName");
            sqlParameter.SqlDbType.Should().Be(SqlDbType.Structured);
        }

        [Fact]
        public void SqlParameterNameIsCorrect()
        {
            var sqlParameter = Generator.GetTestEnumerable().ToSqlParameter("TestName", "typeName");
            sqlParameter.ParameterName.Should().Be("TestName");
        }

        [Fact]
        public void TypeNameNameIsCorrect()
        {
            var sqlParameter = Generator.GetTestEnumerable().ToSqlParameter("TestName", "typeName");
            sqlParameter.TypeName.Should().Be("typeName");
        }

        [Fact]
        public void ValueIsTypeOfDataTable()
        {
            var sqlParameter = Generator.GetTestEnumerable().ToSqlParameter("TestName", "typeName");
            sqlParameter.Value.Should().BeOfType<DataTable>();
        }
    }
}