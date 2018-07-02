using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private readonly int defaultFieldLength = 10;

        [TestMethod]
        public void Should_ConvertStringToBool_When_TypeConverterDefined()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.boolTest)
                    .ToList(),
                typeof(bool));
        }

        [TestMethod]
        public void Should_ParseAllFieldsMatchingExpected_When_ParseFileIsCalled()
        {
            AssertAllRowsMatchExpected();
        }

        [TestMethod]
        public void Should_ParseFirstRowMatchingExpected_When_ParseFileIsCalled()
        {
            AssertFirstRowMatchesExpected();
        }

        [TestMethod]
        public void Should_ReadNumberOfRowsMatchingInputFile_When_ParseFileIsCalled()
        {
            Assert.AreEqual(ParsedRows.Count, 4);
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
                    doubleTest = 36 // testing int to double
                },
                new PrimitiveTypes
                {
                    id = 2,
                    charTest = 'c',
                    stringTest = "string3",
                    boolTest = true, // '1' in test file
                    doubleTest = 123.45678910 // testing decimal to double
                },
                new PrimitiveTypes
                {
                    id = 3,
                    charTest = '.',
                    stringTest = "ABCDEFG",
                    boolTest = true, // 'TRUE' in test file (testing caps)
                    doubleTest = 100000 // '100,000' in test file (testing commas
                }
            };
        }

        protected override string GetFilePath()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\PrimitiveTypesTest.dat"; // file properties should be "Content" and "Copy If Newer" (or similar)
        }

        protected override IFlatFileLayoutDescriptor<PrimitiveTypes> GetLayout()
        {
            return new LayoutDescriptor<PrimitiveTypes>()
                .AppendField(x => x.id, defaultFieldLength)
                .AppendField(x => x.charTest, defaultFieldLength)
                .AppendField(x => x.stringTest, defaultFieldLength)
                .AppendField(x => x.boolTest, defaultFieldLength)
                .AppendField(x => x.doubleTest, 15);
        }
    }
}