using DataMunging.Reporting.Reports;
using DataMunging.Reporting.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMunging.UnitTests
{
    [TestClass]
    public class MinMaxReportTest
    {
        [TestMethod]
        public void Should_GetMaxSoccerSpreadOf43_When_GetLargestPointSpreadIsRun()
        {
            // Arrange
            var report = new SoccerScoreReport<ILeagueScore>(MockedData.GetExpectedScores());

            // Act
            var max = report.GetLargestPointSpread();

            // Assert
            Assert.AreEqual(max, 43);
        }

        [TestMethod]
        public void Should_GetMaxTemperatureOf54_When_GetLargestTemperatureSpreadIsRun()
        {
            // Arrange
            var report = new WeatherReport<IDailyTemperature>(MockedData.GetExpectedTemperatures());

            // Act
            var max = report.GetLargestTemperatureSpread();

            // Assert
            Assert.AreEqual(max, 54); // 86 vs. 32 degrees in weather.dat
        }

        [TestMethod]
        public void Should_GetMinScoreOf1_When_GetSmallestPointSpreadIsRun()
        {
            // Arrange
            var report = new SoccerScoreReport<ILeagueScore>(MockedData.GetExpectedScores());

            // Act
            var min = report.GetSmallestPointSpread();

            // Assert
            Assert.AreEqual(min, 1);
        }

        [TestMethod]
        public void Should_GetMinTemperatureOf2_When_GetSmallestTemperatureSpreadIsRun()
        {
            // Arrange
            var report = new WeatherReport<IDailyTemperature>(MockedData.GetExpectedTemperatures());

            // Act
            var min = report.GetSmallestTemperatureSpread();

            // Assert
            Assert.AreEqual(min, 2);
        }
    }
}