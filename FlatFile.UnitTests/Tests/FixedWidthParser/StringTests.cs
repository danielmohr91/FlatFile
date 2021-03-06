﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.LayoutDescriptor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.FixedWidthParser
{
    [TestClass]
    public class StringTests : ParserTestBase<DummyStringModel>
    {
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
        public void Should_Read250Rows_When_InputFileHas250Rows()
        {
            Assert.AreEqual(ParsedRows.Count, 250);
        }

        protected override ICollection<DummyStringModel> GetExpectedRows()
        {
            ICollection<DummyStringModel> generatedRows = new Collection<DummyStringModel>();
            for (var i = 0; i < 250; i++)
            {
                var rowNumber = i + 1;

                generatedRows.Add(new DummyStringModel
                {
                    Id = rowNumber.ToString(),
                    Field1 = $"Row{rowNumber}Field1",
                    Field2 = $"Row{rowNumber}Field2",
                    Field3 = $"Row{rowNumber}Field3",
                    Field4 = $"Row{rowNumber}Field4"
                });
            }

            return generatedRows;
        }

        protected override string GetFilePath()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\StringTest.dat"; // file properties should be "Content" and "Copy If Newer" (or similar)
        }

        protected override IFlatFileLayoutDescriptor<DummyStringModel> GetLayout()
        {
            return new LayoutDescriptor<DummyStringModel>()
                .AppendField(x => x.Id, DefaultFieldWidth)
                .AppendField(x => x.Field1, DefaultFieldWidth)
                .AppendField(x => x.Field2, DefaultFieldWidth)
                .AppendField(x => x.Field3, DefaultFieldWidth)
                .AppendField(x => x.Field4, DefaultFieldWidth);
        }
    }
}