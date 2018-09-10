using System.Linq;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Implementation.TypeConverters;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    [TestClass]
    public class ConventionBasedTypeConverterTests
    {
        private readonly int fieldLength = 10;

        [TestMethod]
        public void Should_AppendBooleanTypeConverter_When_BooleanFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.boolTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new BooleanTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendDecimalTypeConverter_When_DecimalFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.decimalTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new DecimalTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendDoubleTypeConverter_When_DoubleFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.doubleTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new DoubleTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendFloatTypeConverter_When_FloatFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.floatTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new FloatTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendIntTypeConverter_When_IntFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.intTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new IntTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendLongTypeConverter_When_LongFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.longTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new LongTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendShortTypeConverter_When_ShortFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.shortTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new ShortTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendStringTypeConverter_When_StringFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.stringTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new StringTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendUIntTypeConverter_When_UIntFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.uintTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new UIntTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendULongTypeConverter_When_ULongFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.ulongTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new ULongTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }

        [TestMethod]
        public void Should_AppendUShortTypeConverter_When_UShortFieldIsAppended()
        {
            var layout = new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.ushortTest, fieldLength);

            var field = layout.GetOrderedFields().FirstOrDefault();
            var expectedType = new UShortTypeConverter();

            Assert.IsInstanceOfType(field.TypeConverter, expectedType.GetType());
        }
    }
}