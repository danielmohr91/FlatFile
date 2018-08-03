﻿using System.Collections.Generic;
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
        protected int stringFieldLength = 10;

        [TestMethod]
        public void GenerateTestFile()
        {
            // See test file here: 
            // c:\projects\flatfile\FlatFile.UnitTests\bin\Debug\OutputFiles\PrimitiveTypesOutputTest.dat
            WriteTestFile(GetExpectedRows(), GetLayout());
        }

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
                    boolTest = true,
                    decimalTest = decimal.MaxValue,
                    doubleTest = double.MaxValue,
                    floatTest = float.MaxValue,
                    intTest = int.MaxValue,
                    longTest = long.MaxValue,
                    shortTest = short.MaxValue,
                    stringTest = "Test 1",
                    uintTest = uint.MaxValue,
                    ulongTest = ulong.MaxValue,
                    ushortTest = ushort.MaxValue
                },
                new PrimitiveTypesModel
                {
                    id = 1,
                    boolTest = false, // // 'FALSE' in test file (testing caps)
                    decimalTest = decimal.MinValue,
                    doubleTest = double.MinValue,
                    floatTest = float.MinValue,
                    intTest = int.MinValue,
                    longTest = long.MinValue,
                    shortTest = short.MinValue,
                    stringTest = "Test 2",
                    uintTest = uint.MinValue,
                    ulongTest = ulong.MinValue,
                    ushortTest = ushort.MinValue
                },
                new PrimitiveTypesModel
                {
                    id = 2,
                    boolTest = false, // 0 in test file
                    decimalTest = (decimal) 42.42424242,
                    doubleTest = 42.42424242,
                    floatTest = (float) 42.42424242,
                    intTest = 42,
                    longTest = (long) 42.42424242,
                    shortTest = (short) 42.42424242,
                    stringTest = "l33t $42",
                    uintTest = 42,
                    ulongTest = 42,
                    ushortTest = 42
                },
                new PrimitiveTypesModel
                {
                    id = 3,
                    boolTest = true, // 1 in test file
                    decimalTest = 0,
                    doubleTest = 0,
                    floatTest = 0,
                    intTest = 0,
                    longTest = 0,
                    shortTest = 0,
                    stringTest = string.Empty,
                    uintTest = 0,
                    ulongTest = 0,
                    ushortTest = 0
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
                .AppendField(x => x.stringTest, stringFieldLength)
                .AppendField(x => x.boolTest, defaultFieldLength)
                .AppendField(x => x.doubleTest, 15);
        }

        private string GetOutputFilePath()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\OutputFiles\\PrimitiveTypesOutputTest.dat";
        }

        private void WriteTestFile(ICollection<PrimitiveTypesModel> rows, IFlatFileLayoutDescriptor<PrimitiveTypesModel> layout)
        {
            var writer = new FixedWidthFileWriter<PrimitiveTypesModel>(layout, GetOutputFilePath());
            writer.WriteFile(rows);
        }
    }
}