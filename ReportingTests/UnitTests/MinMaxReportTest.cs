using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using DataMunging.Reporting.Import;
using DataMunging.Reporting.Reports;
using DataMunging.Reporting.TestForSkip;
using DataMunging.Reporting.Transformations;
using DataMunging.Reporting.ViewModels;
using FlatFile.FixedWidth.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMunging.UnitTests
{
    [TestClass]
    public class MinMaxReportTest
    {
        [TestMethod]
        public void Should_GetMinTemperatureMatchingExpected_When_MinMaxReportIsRun()
        {
            // Arrange
            var report = new ReportMinMaxTemperature<>(MockedData.GetExpectedTemperatures());

            // Act
            var min = report.GetMinSpread();

            // Assert
            Assert.AreEqual(min, 2);
        }

        [TestMethod]
        public void Should_GetMaxTemperatureMatchingExpected_When_MinMaxReportIsRun()
        {
            // Arrange
            var report = new ReportMinMaxTemperature<IEnumerable<IDailyTemperatures>>(MockedData.GetExpectedTemperatures());

            // Act
            var max = report.GetMaxSpread();

            // Assert
            Assert.AreEqual(max, 54); // 86 vs. 32 degrees in weather.dat
        }

        [TestMethod]
        public void Should_GetMinScoreMatchingExpected_When_MinMaxReportIsRun()
        {
            // Arrange
            var report = new ReportMinMaxTemperature<IEnumerable<IDailyTemperatures>>(MockedData.GetExpectedScores());

            // Act
            var min = report.GetMinSpread();

            // Assert
            Assert.AreEqual(min, 2);
        }

        [TestMethod]
        public void Should_GetMaxScoreMatchingExpected_When_MinMaxReportIsRun()
        {
            // Arrange
            var report = new ReportMinMaxTemperature<IEnumerable<IDailyTemperatures>>(MockedData.GetExpectedScores());

            // Act
            var max = report.GetMaxSpread();

            // Assert
            Assert.AreEqual(max, 54); // 86 vs. 32 degrees in weather.dat
        }
    }
}