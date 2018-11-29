using System;
using System.Collections.Generic;
using System.Linq;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Reporting.Reports
{
    public class WeatherReport<T> where T : IDailyTemperature
    {
        private readonly IEnumerable<T> dailyTemperatures;
        private IList<Tuple<IDailyTemperature, int>> differences;


        public WeatherReport(IEnumerable<T> dailyTemperatures)
        {
            this.dailyTemperatures = dailyTemperatures;
        }

        public IList<Tuple<IDailyTemperature, int>> Differences
        {
            get => differences ?? (differences = GetDifferences());
            set => differences = value;
        }

        public IDailyTemperature GetDayWithLargestSpread()
        {
            return Differences.LastOrDefault()?.Item1;
        }

        public IDailyTemperature GetDayWithSmallestSpread()
        {
            return Differences.FirstOrDefault()?.Item1;
        }

        public int GetLargestTemperatureSpread()
        {
            return Differences.LastOrDefault()?.Item2 ?? 0;
        }

        public int GetSmallestTemperatureSpread()
        {
            return Differences.FirstOrDefault()?.Item2 ?? 0;
        }

        private IList<Tuple<IDailyTemperature, int>> GetDifferences()
        {
            return dailyTemperatures
                .Select(x => new Tuple<IDailyTemperature, int>(x, Math.Abs(x.HighTemperature - x.LowTemperature)))
                .OrderBy(x => x)
                .ToList();
        }
    }
}