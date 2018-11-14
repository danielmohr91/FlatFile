using System;
using System.Collections.Generic;
using System.Linq;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Reporting.Reports
{
    // Given a collection of DayDayId, Min, and Max values, report min and max spreads
    public class WeatherReport<T> where T : IDailyTemperature
    {
        private readonly MinMaxReport minMaxReport;

        public WeatherReport(IEnumerable<T> points)
        {
            minMaxReport = new MinMaxReport(points.Select(x => new Tuple<int, int>(x.LowTemperature, x.HighTemperature)));
        }

        public int GetLargestTemperatureSpread()
        {
            return minMaxReport.GetMaxSpread();
        }

        public int GetSmallestTemperatureSpread()
        {
            return minMaxReport.GetMinSpread();
        }
    }
}