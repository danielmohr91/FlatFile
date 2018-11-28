using System;
using System.Collections.Generic;
using System.Linq;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Reporting.Reports
{
    public class WeatherReport<T> where T : IDailyTemperature
    {
        private readonly IEnumerable<T> dailyTemperatures;
        private IList<int> differences;


        public WeatherReport(IEnumerable<T> dailyTemperatures)
        {
            this.dailyTemperatures = dailyTemperatures;
        }
        
        public IList<int> Differences
        {
            get => differences ?? (differences = GetDifferences());
            set => differences = value;
        }

        public int GetLargestTemperatureSpread()
        {
            return Differences.LastOrDefault();
        }

        public int GetSmallestTemperatureSpread()
        {
            return Differences.FirstOrDefault();
        }

        private IList<int> GetDifferences()
        {
            return dailyTemperatures
                .Select(x => Math.Abs(x.HighTemperature = x.LowTemperature))
                .OrderBy(x => x)
                .ToList();
        }
    }
}