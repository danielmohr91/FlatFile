using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.TypeConverter.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.TypeConverter
{
    [TestClass]
    public class FixedWidthFileWriterTest
    {
        private readonly IFlatFileLayoutDescriptor<PrimitiveTypesModel> layout;
        private readonly Collection<PrimitiveTypesModel> rows;

        public FixedWidthFileWriterTest()
        {
            var logic = new PrimitiveTypeExpectations();
            rows = logic.GetExpectedRows();
            layout = logic.GetLayout();
        }

        [TestMethod]
        public void Should_GenerateAFile_When_WriteTestFileIsCalled()
        {
            // See test file here: 
            // c:\projects\flatfile\FlatFile.UnitTests\bin\Debug\OutputFiles\PrimitiveTypesOutputTest.dat

            // No guarantee is made to correctness. 
            // Copy / Paste the results from the OutputFiles\PrimitiveTypesOutputTest.dat into InputFiles\PrimitiveTypesTest.dat to test this
            File.Delete(GetOutputFilePath());
            WriteTestFile(rows, layout);
            Assert.IsTrue(File.Exists(GetOutputFilePath()));
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