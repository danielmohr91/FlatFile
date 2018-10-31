using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using DataMunging.Reporting.Import;
using DataMunging.Reporting.TestForSkip;
using DataMunging.Reporting.Transformations;
using DataMunging.Reporting.ViewModels;
using FlatFile.FixedWidth.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMunging.UnitTests
{
    [TestClass]
    public class WeatherImporterTests
    {
        [TestMethod]
        public void Should_ImportFlatFileToModel_When_LayoutDescriptorIsDefined()
        {
            // Arrange
            if (!File.Exists(GetOriginalImportFilePath()))
            {
                throw new Exception("Import file does not exist!");
            }
            var expected = MockedData.GetExpectedPoints();
            var testForSkip = new WeatherReportSkipDefinitions();
            
            // Act
            var importer = new WeatherImporter(GetOriginalImportFilePath());
            var model = importer.GetRows(testForSkip).ToList();

            // Assert
            CollectionAssert.AreEqual(model, expected);
        }



        private string GetOriginalImportFilePath()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\weather.dat";
        }
    }
}