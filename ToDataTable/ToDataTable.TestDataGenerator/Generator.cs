using System;
using System.Collections.Generic;

namespace ToDataTable.TestDataGenerator
{
    public static class Generator
    {
        public static TestClass GetTestClassType1()
        {
            return new TestClass
            {
                Int = 1,
                NullableInt = default,
                String = Guid.NewGuid().ToString(),
                Guid = Guid.NewGuid(),
                NullableGuid = default,
                DateTime = new DateTime(2020, 1, 1),
                NullableDateTime = default,
                Binary = Guid.NewGuid().ToByteArray(),
                Boolean = true,
                Int16 = 2,
                Int64 = 3,
                Decimal = new decimal(1.1),
                Single = 4,
                Double = 5
            };
        }
        
        public static PrecompiledTestClass GetPrecompiledTestClass()
        {
            return new PrecompiledTestClass
            {
                Int = 1,
                NullableInt = default,
                String = Guid.NewGuid().ToString(),
                Guid = Guid.NewGuid(),
                NullableGuid = default,
                DateTime = new DateTime(2020, 1, 1),
                NullableDateTime = default,
                Binary = Guid.NewGuid().ToByteArray(),
                Boolean = true,
                Int16 = 2,
                Int64 = 3,
                Decimal = new decimal(1.1),
                Single = 4,
                Double = 5
            };
        }

        public static TestClass GetTestClassType2()
        {
            return new TestClass
            {
                Int = 11,
                NullableInt = default,
                String = Guid.NewGuid().ToString(),
                Guid = Guid.NewGuid(),
                NullableGuid = default,
                DateTime = new DateTime(2021, 2, 2),
                NullableDateTime = default,
                Binary = Guid.NewGuid().ToByteArray(),
                Boolean = true,
                Int16 = 21,
                Int64 = 31,
                Decimal = new decimal(11.11),
                Single = 41,
                Double = 51
            };
        }

        public static IEnumerable<TestClass> GetTestEnumerable()
        {
            return new[]
            {
                GetTestClassType1(),
                GetTestClassType2()
            };
        }

        public static IEnumerable<TestClass> GetTestEnumerable(int num)
        {
            var list = new List<TestClass>();
            for (var i = 0; i < num; i++)
            {
                if (i % 2 == 0)
                {
                    list.Add(GetTestClassType1());
                }
                else
                {
                    list.Add(GetTestClassType2());
                }
            }

            return list;
        }

        public static IEnumerable<PrecompiledTestClass> GetPrecompiledTestEnumerable(int num)
        {
            var list = new List<PrecompiledTestClass>();
            for (var i = 0; i < num; i++)
            {
                list.Add(GetPrecompiledTestClass());
            }

            return list;
        }
    }
}