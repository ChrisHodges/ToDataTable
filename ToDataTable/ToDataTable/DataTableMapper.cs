using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace ToDataTable
{
    public class DataTableMapper
    {
        private DataTable CreateDataTable(IEnumerable<DataRowBuilder> dataRowBuilders)
        {
            DataTable table = new DataTable();
            foreach (var builder in dataRowBuilders)
            {
                table.Columns.Add(builder.Name, builder.Type);
            }
            return table;
        }

        private void AddDataRow(DataTable table, IEnumerable<DataRowBuilder> dataRowBuilders, object item)
        {
            DataRow row = table.NewRow();
            foreach (var builder in dataRowBuilders)
                row[builder.Name] = builder.Getter(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        
        public DataTable ToDataTable<T>(IEnumerable<T> enumerable, IToDataTableContext toDataTableContext)
        {
            var dataRowBuilders = toDataTableContext.GetDataRowBuilders<T>();
            if (dataRowBuilders == null)
            {
                dataRowBuilders = toDataTableContext.SetDataRowBuilders<T>(TypeDescriptor.GetProperties(typeof(T)));
            }

            var table = CreateDataTable(dataRowBuilders);

            foreach (T item in enumerable)
            {
                AddDataRow(table, dataRowBuilders, item);
            }
            return table;
        }
    }
}