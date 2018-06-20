using System;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Infrastructure.Mocks;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    [TestClass]
    public class TypeConverterTests
    {
        private readonly int fieldLength = 10;

        private readonly IFlatFileLayoutDescriptor<PrimitiveTypes> settings;

        public TypeConverterTests()
        {
            settings = GetTestLayoutDescriptorForPrimitiveTypes();
        }

        [TestMethod]
        public void Should_ConvertBoolToString_When_TypeConverterDefined()
        {
            var parser = new DummyFixedWidthFileParser<PrimitiveTypes>(settings);
            parser.ParseFile();

            throw new NotImplementedException("TODO: resume here testing type converter when Flat File Parser is implemented and tested.");
        }

        private IFlatFileLayoutDescriptor<PrimitiveTypes> GetTestLayoutDescriptorForPrimitiveTypes()
        {
            return new LayoutDescriptor<PrimitiveTypes>()
                .AppendField(x => x.id, fieldLength)
                .AppendField(x => x.boolTest, fieldLength)
                .AppendField(x => x.charTest, fieldLength)
                .AppendField(x => x.doubleTest, fieldLength)
                .AppendField(x => x.stringTest, fieldLength);
        }
    }
}