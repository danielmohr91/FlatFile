using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.CustomTypeConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.TypeConverter
{
    [TestClass]
    public class CustomTypeConvertTests

    {
        private readonly IGenericTypeConverter converter;
        private readonly IFlatFileLayoutDescriptor<Day> settings;

        public CustomTypeConvertTests()
        {

        }
    }
}