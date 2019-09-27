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
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in enumerable)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}