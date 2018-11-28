using DataMunging.Reporting.Reports;
using DataMunging.Reporting.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMunging.UnitTests
{
    [TestClass]
    public class SoccerScoreReportTests
    {
        [TestMethod]
        public void Should_GetMaxSoccerSpreadOf43_When_GetLargestPointSpreadIsRun()
        {
            // Arrange
            var report = new SoccerScoreReport<ILeagueScore>(MockedData.GetExpectedScores());

            // Act
            var max = report.GetLargestPointSpread();

            // Assert
            Assert.AreEqual(43, max);
        }

        [TestMethod]
        public void Should_GetMinScoreOf1_When_GetSmallestPointSpreadIsRun()
        {
            // Arrange
            var report = new SoccerScoreReport<ILeagueScore>(MockedData.GetExpectedScores());

            // Act
            var min = report.GetSmallestPointSpread();

            // Assert
            Assert.AreEqual(1, min);
        }
    }
}