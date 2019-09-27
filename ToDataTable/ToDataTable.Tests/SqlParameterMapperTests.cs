using System.Data;
using System.Data.SqlClient;
using FluentAssertions;
using ToDataTable.TestDataGenerator;
using Xunit;

namespace ToDataTable.Tests
{
    public class SqlParameterMapperTests
    {
        [Fact]
        public void SqlParameterNameIsCorrect()
        {
            SqlParameter sqlParameter = Generator.GetTestEnumerable().ToSqlParameter("TestName","typeName");
            sqlParameter.ParameterName.Should().Be("TestName");
        }
        
        [Fact]
        public void TypeNameNameIsCorrect()
        {
            SqlParameter sqlParameter = Generator.GetTestEnumerable().ToSqlParameter("TestName","typeName");
            sqlParameter.TypeName.Should().Be("typeName");
        }

        [Fact]
        public void DbTypeIsCorrect()
        {
            SqlParameter sqlParameter = Generator.GetTestEnumerable().ToSqlParameter("TestName","typeName");
            sqlParameter.SqlDbType.Should().Be(SqlDbType.Structured);
        }

        [Fact]
        public void ValueIsTypeOfDataTable()
        {
            SqlParameter sqlParameter = Generator.GetTestEnumerable().ToSqlParameter("TestName","typeName");
            sqlParameter.Value.Should().BeOfType<DataTable>();
        }
    }
}