using System.ComponentModel;
using System.Linq;
using FluentAssertions;
using ToDataTable.TestDataGenerator;
using Xunit;

namespace ToDataTable.Tests
{
    public class ToDataTableContextTests
    {
        [Fact]
        public void PrecompileAttributeWorks()
        {
            ToDataTableContext.PrecompileMaps(typeof(PrecompiledTestClass).Assembly);
            ToDataTableContext.Instance.CachedTypes.Should().Contain(typeof(PrecompiledTestClass));
        }

        [Fact]
        public void SetDataRowBuildersSetsCorrectNumberOfProperties()
        {
            IToDataTableContext dataTableContext = ToDataTableContext.Instance;
            var result = dataTableContext.SetDataRowBuilders<TestClass>(TypeDescriptor.GetProperties(typeof(TestClass)));
            result.Count().Should().Be(14);
        }
        
        [Fact]
        public void CreateDataRowBuilderFromPropertyDescriptorWorks()
        {
            var dataTableContext = ToDataTableContext.Instance;
            var properties = TypeDescriptor.GetProperties(typeof(TestClass));
            var dataRowBuilder =
                dataTableContext.CreateDataRowBuilderFromPropertyDescriptor(properties[0], typeof(TestClass));
            dataRowBuilder.Name.Should().Be("Int");
            dataRowBuilder.Type.Should().Be(typeof(int));
            var testClass = new TestClass
            {
                Int = 999
            };
            dataRowBuilder.Getter(testClass).Should().Be(999);

        }
    }
}