using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace ToDataTable
{
    public static class ToDataTableExtensionMethods
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable)
        {
            return DataTableMapper.ToDataTable(enumerable, ToDataTableContext.Instance);
        }

        public static SqlParameter ToSqlParameter<T>(this IEnumerable<T> enumerable, string parameterName,
            string typeName)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlValue = enumerable.ToDataTable(),
                SqlDbType = SqlDbType.Structured,
                TypeName = typeName
            };
        }
    }
}