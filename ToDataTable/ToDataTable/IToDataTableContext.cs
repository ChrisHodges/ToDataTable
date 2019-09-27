using System.Collections.Generic;
using System.ComponentModel;

namespace ToDataTable
{
    public interface IToDataTableContext
    {
        IEnumerable<DataRowBuilder> GetDataRowBuilders<T>();
        IEnumerable<DataRowBuilder> SetDataRowBuilders<T>(PropertyDescriptorCollection getProperties);
    }
}