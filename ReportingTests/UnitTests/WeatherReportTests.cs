using DataMunging.Reporting.Reports;
using DataMunging.Reporting.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMunging.UnitTests
{
    [TestClass]
    public class WeatherReportTests
    {
        [TestMethod]
        public void Should_GetMaxTemperatureOf54_When_GetLargestTemperatureSpreadIsRun()
        {
            // Arrange
            var report = new WeatherReport<IDailyTemperature>(MockedData.GetExpectedTemperatures());

            // Act
            var max = report.GetLargestTemperatureSpread();

            // Assert
            Assert.AreEqual(54, max); // 86 vs. 32 degrees in weather.dat
        }

        [TestMethod]
        public void Should_GetMinTemperatureOf2_When_GetSmallestTemperatureSpreadIsRun()
        {
            // Arrange
            var report = new WeatherReport<IDailyTemperature>(MockedData.GetExpectedTemperatures());

            // Act
            var min = report.GetSmallestTemperatureSpread();

            // Assert
            Assert.AreEqual(2, min);
        }
    }
}