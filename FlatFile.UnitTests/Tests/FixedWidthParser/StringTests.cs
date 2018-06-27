using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FlatFile.FixedWidth.Implementation;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.FixedWidthParser
{
    [TestClass]
    public class StringTests
    {
        private readonly ICollection<DummyStringModel> expectedRows;
        private readonly int fieldWidth = 15;
        private readonly ICollection<DummyStringModel> parsedRows;

        public StringTests()
        {
            expectedRows = GetExpectedRows();
            parsedRows = ParseTestFile();
        }

        [TestMethod]
        public void Should_ParseAllFieldsMatchingExpected_When_ParseFileIsCalled()
        {
            CollectionAssert.AreEqual((ICollection) expectedRows, (ICollection) parsedRows);
        }

        [TestMethod]
        public void Should_ParseFirstRowMatchingExpected_When_ParseFileIsCalled()
        {
            DummyStringModel expected, actual;
            expected = expectedRows.First();
            actual = parsedRows.First();

            // This is reference equals by default. Equals method is overriden in DummyStringModel to implement value equals vs. reference equals
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Should_Read250Rows_When_InputFileHas250Rows()
        {
            var allRows = ParseTestFile();
            Assert.AreEqual(allRows.Count, 250);
        }

        [TestMethod]
        public void Should_PopulateFiveColumns_When_LayoutDescriptorDefinesFiveFields()
        {
            // Make sure first five columns have values, and that nothing more and nothing less is being set. 
            foreach (var record in parsedRows)
            {
                Assert.IsTrue(
                    string.IsNullOrEmpty(record.Field1) &&
                    string.IsNullOrEmpty(record.Field2) &&
                    string.IsNullOrEmpty(record.Field3) &&
                    string.IsNullOrEmpty(record.Field4) &&
                    string.IsNullOrEmpty(record.Field5) &&
                    record.Field6 == null &&
                    record.Field7 == null &&
                    record.Field8 == null &&
                    record.Field9 == null &&
                    record.Field10 == null &&
                    record.Field11 == null &&
                    record.Field12 == null);
            }
        }

        private ICollection<DummyStringModel> GetExpectedRows()
        {
            ICollection<DummyStringModel> generatedRows = new Collection<DummyStringModel>();
            for (var i = 0; i < 250; i++)
            {
                var rowNumber = i + 1;
                generatedRows.Add(new DummyStringModel
                {
                    Id = rowNumber.ToString().PadLeft(fieldWidth),
                    Field1 = $"Row{rowNumber}Field1".PadRight(fieldWidth),
                    Field2 = $"Row{rowNumber}Field2".PadRight(fieldWidth),
                    Field3 = $"Row{rowNumber}Field3".PadRight(fieldWidth),
                    Field4 = $"Row{rowNumber}Field4".PadRight(fieldWidth)
                });
            }

            return generatedRows;
        }

        private ICollection<DummyStringModel> ParseTestFile()
        {
            var layout = new LayoutDescriptor<DummyStringModel>()
                .AppendField(x => x.Id, fieldWidth)
                .AppendField(x => x.Field1, fieldWidth)
                .AppendField(x => x.Field2, fieldWidth)
                .AppendField(x => x.Field3, fieldWidth)
                .AppendField(x => x.Field4, fieldWidth);

            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = $"{directory}\\InputFiles\\StringTest.dat"; // file properties should be "Content" and "Copy If Newer" (or similar)

            var parser = new FixedWidthFileParser<DummyStringModel, Collection<DummyStringModel>>(layout, path);
            return parser.ParseFile();
        }
    }
}