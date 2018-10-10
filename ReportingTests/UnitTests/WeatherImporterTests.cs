using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using DataMunging.Reporting.Import;
using DataMunging.Reporting.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMunging.UnitTests
{
    [TestClass]
    public class WeatherImporterTests
    {
        [TestMethod]
        public void Should_ImportFlatFileToModel_When_LayoutDescriptorIsDefined()
        {
            var importer = new WeatherImporter(GetImportFilePath());
            var model = importer.GetRows();

            // TODO: Mock expected result based on ~\DataMunging\UnitTests\InputFiles\weather.dat
            var expected = new Collection<Point>
            {
                new Point(1, 88, 59),
                new Point(2, 79, 63),
                new Point(3, 77, 55),
                new Point(4, 77, 59),
                new Point(5, 90, 66),
                new Point(6, 81, 61),
                new Point(7, 73, 57),
                new Point(8, 75, 54),
                new Point(9, 86, 32),
                new Point(10, 84, 64),
                new Point(11, 91, 59),
                new Point(12, 88, 73),
                new Point(13, 70, 59),
                new Point(14, 61, 59),
                new Point(15, 64, 55),
                new Point(16, 79, 59),
                new Point(17, 81, 57),
                new Point(18, 82, 52),
                new Point(19, 81, 61),
                new Point(20, 84, 57),
                new Point(21, 86, 59),
                new Point(22, 90, 64),
                new Point(23, 90, 68),
                new Point(24, 90, 77),
                new Point(25, 90, 72),
                new Point(26, 97, 64),
                new Point(27, 91, 72),
                new Point(28, 84, 68),
                new Point(29, 88, 66),
                new Point(30, 90, 45)
            };

            CollectionAssert.AreEqual(model.ToList(), expected);

            // The totals row is problematic. 
            // Reading line by line, unsure of what is last row since using a stream - it's not trivial to simply pass flag for IgnoreLastRow
            // Is there a better way? 
            // These totals at the bottom should be ignored: 
            //   mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3
            // Perhaps want to run file through a pre-processor? 

        }

        private string GetImportFilePath()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\weather.dat";
        }
    }
}