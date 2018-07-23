using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.LayoutDescriptor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.TypeConverter
{
    [TestClass]
    public class PrimitiveTypesTest : ParserTestBase<PrimitiveTypesModel>
    {
        private readonly int defaultFieldLength = 10;

        [TestMethod]
        public void Should_ConvertStringToBool_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.boolTest)
                    .ToList(),
                typeof(bool));
        }

        [TestMethod]
        public void Should_ConvertStringToByte_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.byteTest)
                    .ToList(),
                typeof(byte));
        }


        [TestMethod]
        public void Should_ConvertStringToChar_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.charTest)
                    .ToList(),
                typeof(char));
        }


        [TestMethod]
        public void Should_ConvertStringToDecimal_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.decimalTest)
                    .ToList(),
                typeof(decimal));
        }

        [TestMethod]
        public void Should_ConvertStringToDouble_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.doubleTest)
                    .ToList(),
                typeof(double));
        }

        [TestMethod]
        public void Should_ConvertStringToFloat_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.floatTest)
                    .ToList(),
                typeof(float));
        }

        [TestMethod]
        public void Should_ConvertStringToInt_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.intTest)
                    .ToList(),
                typeof(int));
        }

        [TestMethod]
        public void Should_ConvertStringToLong_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.longTest)
                    .ToList(),
                typeof(long));
        }

        [TestMethod]
        public void Should_ConvertStringToSByte_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.sbyteTest)
                    .ToList(),
                typeof(sbyte));
        }

        [TestMethod]
        public void Should_ConvertStringToShort_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.shortTest)
                    .ToList(),
                typeof(short));
        }

        [TestMethod]
        public void Should_ConvertStringToString_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.stringTest)
                    .ToList(),
                typeof(string));
        }

        [TestMethod]
        public void Should_ConvertStringToUInt_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.uintTest)
                    .ToList(),
                typeof(uint));
        }

        [TestMethod]
        public void Should_ConvertStringToULong_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.ulongTest)
                    .ToList(),
                typeof(ulong));
        }

        [TestMethod]
        public void Should_ConvertStringToUShort_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.ushortTest)
                    .ToList(),
                typeof(ushort));
        }


        [TestMethod]
        public void Should_ParseAllFieldsMatchingExpected_When_ParseFileIsCalled()
        {
            AssertAllRowsMatchExpected();
        }


        [TestMethod]
        public void Should_ReadNumberOfRowsMatchingInputFile_When_ParseFileIsCalled()
        {
            Assert.AreEqual(ParsedRows.Count, 4);
        }

        protected override ICollection<PrimitiveTypesModel> GetExpectedRows()
        {
            return new Collection<PrimitiveTypesModel>
            {
                new PrimitiveTypesModel
                {
                    id = 0,
                    charTest = 'a',
                    stringTest = "string1",
                    boolTest = true,
                    doubleTest = 42
                },
                new PrimitiveTypesModel
                {
                    id = 1,
                    charTest = 'b',
                    stringTest = "string2",
                    boolTest = false,
                    doubleTest = 36 // testing int to double
                },
                new PrimitiveTypesModel
                {
                    id = 2,
                    charTest = 'c',
                    stringTest = "string3",
                    boolTest = true, // '1' in test file
                    doubleTest = 123.45678910 // testing decimal to double
                },
                new PrimitiveTypesModel
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

        protected override IFlatFileLayoutDescriptor<PrimitiveTypesModel> GetLayout()
        {
            return new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.id, defaultFieldLength)
                .AppendField(x => x.charTest, defaultFieldLength)
                .AppendField(x => x.stringTest, defaultFieldLength)
                .AppendField(x => x.boolTest, defaultFieldLength)
                .AppendField(x => x.doubleTest, 15);
        }
    }
}