using FlatFile.FixedWidth.Implementation.TypeConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    [TestClass]
    public class GenericCollectionTests
    {
        [TestMethod]
        public void Should_SuccessfullyCastIntTypeConverterToObject_When_CastIsUsed()
        {
            var intConverter = new IntTypeConverter();
            var genericConverter = intConverter;
        }
    }
}