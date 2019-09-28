using System;

namespace ToDataTable
{
    internal class DataRowBuilder
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public Func<object, object> Getter { get; set; }
    }
}