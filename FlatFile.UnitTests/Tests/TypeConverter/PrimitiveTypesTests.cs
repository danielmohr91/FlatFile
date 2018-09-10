using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.LayoutDescriptor;
using FlatFileParserUnitTests.Tests.TypeConverter.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.TypeConverter
{
    [TestClass]
    public class PrimitiveTypesTests : ParserTestBase<PrimitiveTypesModel>

    {
        [TestMethod]
        public void Should_ConvertCollectionOfStringToBool_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.boolTest)
                    .ToList(),
                typeof(bool));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToDecimal_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.decimalTest)
                    .ToList(),
                typeof(decimal));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToDouble_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.doubleTest)
                    .ToList(),
                typeof(double));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToFloat_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.floatTest)
                    .ToList(),
                typeof(float));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToInt_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.intTest)
                    .ToList(),
                typeof(int));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToLong_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.longTest)
                    .ToList(),
                typeof(long));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToShort_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.shortTest)
                    .ToList(),
                typeof(short));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToString_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.stringTest)
                    .ToList(),
                typeof(string));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToUInt_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.uintTest)
                    .ToList(),
                typeof(uint));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToULong_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.ulongTest)
                    .ToList(),
                typeof(ulong));
        }

        [TestMethod]
        public void Should_ConvertCollectionOfStringToUShort_When_DefaultTypeConverterIsUsed()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.ushortTest)
                    .ToList(),
                typeof(ushort));
        }

        [TestMethod]
        public void Should_ParseCollectionOfFieldsMatchingExpected_When_ParseFileIsCalled()
        {
            AssertAllRowsMatchExpected();
        }

        [TestMethod]
        public void Should_ParseFirstRowMatchingExpected_When_ParseFileIsCalled()
        {
            AssertFirstRowMatchesExpected();
        }


        [TestMethod]
        public void Should_ParseSecondRowMatchingExpected_When_ParseFileIsCalled()
        {
            AssertRowMatchesExpected(2);
        }


        [TestMethod]
        public void Should_ParseThirdRowMatchingExpected_When_ParseFileIsCalled()
        {
            AssertRowMatchesExpected(3);
        }

        [TestMethod]
        public void Should_ReadNumberOfRowsMatchingInputFile_When_ParseFileIsCalled()
        {
            Assert.AreEqual(ParsedRows.Count, ExpectedRows.Count);
        }

        protected override ICollection<PrimitiveTypesModel> GetExpectedRows()
        {
            var expected = new PrimitiveTypeExpectations();
            return expected.GetExpectedRows();
        }

        protected override string GetFilePath()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\PrimitiveTypesTests.dat"; // file properties should be "Content" and "Copy If Newer" (or similar)
        }

        protected override IFlatFileLayoutDescriptor<PrimitiveTypesModel> GetLayout()
        {
            var expected = new PrimitiveTypeExpectations();
            return expected.GetLayout();
        }
    }
}