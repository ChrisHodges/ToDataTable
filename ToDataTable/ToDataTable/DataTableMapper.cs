using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace ToDataTable
{
    internal static class DataTableMapper
    {
        private static DataTable CreateDataTable(IEnumerable<DataRowBuilder> dataRowBuilders)
        {
            DataTable table = new DataTable();
            foreach (var builder in dataRowBuilders)
            {
                table.Columns.Add(builder.Name, builder.Type);
            }
            return table;
        }

        private static void AddDataRow(DataTable table, IEnumerable<DataRowBuilder> dataRowBuilders, object item)
        {
            DataRow row = table.NewRow();
            foreach (var builder in dataRowBuilders)
                row[builder.Name] = builder.Getter(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        
        internal static DataTable ToDataTable<T>(IEnumerable<T> enumerable, IToDataTableContext toDataTableContext)
        {
            var dataRowBuilders = toDataTableContext.GetDataRowBuilders<T>() ?? toDataTableContext.SetDataRowBuilders<T>(TypeDescriptor.GetProperties(typeof(T)));

            var table = CreateDataTable(dataRowBuilders);

            foreach (T item in enumerable)
            {
                AddDataRow(table, dataRowBuilders, item);
            }
            return table;
        }
    }
}