using System;
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
        public void Should_GetMinMatchingExpected_When_MinMaxReportIsRun()
        {
            // Arrange
            var report = new ReportMinMax(MockedData.GetExpectedPoints());

            // Act
            var min = report.GetMinSpread();

            // Assert
            Assert.AreEqual(min, 2);
        }

        [TestMethod]
        public void Should_GetMaxMatchingExpected_When_MinMaxReportIsRun()
        {
            // Arrange
            var report = new ReportMinMax(MockedData.GetExpectedPoints());

            // Act
            var max = report.GetMaxSpread();

            // Assert
            Assert.AreEqual(max, 54);
        }
    }
}