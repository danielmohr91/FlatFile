using System;
using System.IO;
using System.Reflection;
using DataMunging.Reporting.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMunging.UnitTests
{
    [TestClass]
    public class LeagueStandingsImporterTests
    {
        [TestMethod]
        public void Should_ImportFlatFileToModel_When_LayoutDescriptorIsDefined()
        {
            // Arrange
            if (!File.Exists(GetOriginalImportFilePath()))
            {
                throw new Exception("Import file does not exist!");
            }

            var expected = MockedData.GetExpectedScores();

            // Act
            var importer = new SoccerScoreImporter(GetOriginalImportFilePath());
            var model = importer.GetScores();

            // Assert
            CollectionAssert.AreEqual(model, expected);
        }


        private string GetOriginalImportFilePath()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\football.dat";
        }
    }
}