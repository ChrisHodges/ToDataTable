using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace ToDataTable.Benchmarks
{
    public static class ToDataTableBenchmarkingExtensionMethods
    {
        public static DataTable ToDataTableVersion1<T>(this IEnumerable<T> enumerable)
        {
            var properties =
                TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in enumerable)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}