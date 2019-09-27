using System;

namespace ToDataTable.TestDataGenerator
{
    public class TestClass
    {
        public int Int { get; set; }
        public int? NullableInt { get; set; }
        public string String { get; set; }
        public Guid Guid { get; set; }
        public Guid? NullableGuid { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? NullableDateTime { get; set; }
        public Byte[] Binary { get; set; }
        public bool Boolean { get; set; }
        public Int16 Int16 { get; set; }
        public Int64 Int64 { get; set; }
        public decimal Decimal { get; set; }
        public Single Single { get; set; }
        public Double Double { get; set; }
    }
}