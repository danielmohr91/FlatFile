using System;
using System.IO;
using System.Reflection;
using DataMunging.Reporting.Import;
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

            var expected = MockedData.GetExpectedTemperatures();

            // Act
            var importer = new WeatherImporter(GetOriginalImportFilePath());
            var model = importer.GetWeatherReport();

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