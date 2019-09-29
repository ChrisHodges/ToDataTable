using System;

namespace ToDataTable.TestDataGenerator
{
    [PrecompileDataTableMap]
    public class PrecompiledTestClass
    {
        public int Int { get; set; }
        public int? NullableInt { get; set; }
        public string String { get; set; }
        public Guid Guid { get; set; }
        public Guid? NullableGuid { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? NullableDateTime { get; set; }
        public byte[] Binary { get; set; }
        public bool Boolean { get; set; }
        public short Int16 { get; set; }
        public long Int64 { get; set; }
        public decimal Decimal { get; set; }
        public float Single { get; set; }
        public double Double { get; set; }
    }
}