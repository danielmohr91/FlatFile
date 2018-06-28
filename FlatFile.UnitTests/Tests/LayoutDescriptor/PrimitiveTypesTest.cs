using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    [TestClass]
    public class PrimitiveTypesTest : ParserTestBase<PrimitiveTypes>
    {
        private readonly int fieldLength = 10;
        private ICollection<PrimitiveTypes> parsedRows;

        [TestMethod]
        public void Should_ConvertStringToBool_When_TypeConverterDefined()
        {
            Assert.IsInstanceOfType(parsedRows.First().boolTest, typeof(bool));
        }

        [TestMethod]
        public void Should_Read2Rows_When_InputFileHas2Rows()
        {
            Assert.AreEqual(ParsedRows.Count, 2);
        }

        [TestMethod]
        public void Should_ParseFirstRowMatchingExpected_When_ParseFileIsCalled()
        {
            AssertFirstRowMatchesExpected();
        }

        [TestMethod]
        public void Should_ParseAllFieldsMatchingExpected_When_ParseFileIsCalled()
        {
            AssertAllRowsMatchExpected();
        }

        protected override ICollection<PrimitiveTypes> GetExpectedRows()
        {
            return new Collection<PrimitiveTypes>
            {
                new PrimitiveTypes
                {
                    id = 0,
                    charTest = 'a',
                    stringTest = "string1",
                    boolTest = true,
                    doubleTest = 42
                },
                new PrimitiveTypes
                {
                    id = 1,
                    charTest = 'b',
                    stringTest = "string2",
                    boolTest = false,
                    doubleTest = 36
                }
            };
        }

        protected override string GetFilePath()
        {
            throw new System.NotImplementedException();
        }

        protected override IFlatFileLayoutDescriptor<PrimitiveTypes> GetLayout()
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