using System;
using System.Collections.Generic;
using System.Text;
using FlatFile.FixedWidth.Interfaces;

namespace DataMunging.Reporting.TestForSkip
{
    public class WeatherReportSkipDefinitions : ITestForSkip
    {
        public bool ShouldSkip(string row)
        {
            return row.StartsWith("  mo");
        }
    }
}
