using System.Linq;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Implementation.TypeConverters;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    [TestClass]
    public class ConventionBasedTypeConverterTests
    {
        private int fieldLength = 10;

        [TestMethod]
        public void Should_AppendIntTypeConverter_When_IntFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.intTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var intTypeConverter = new IntTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, intTypeConverter.GetType());
        }

        // TODO: Check remaining primitive types here
    }
}