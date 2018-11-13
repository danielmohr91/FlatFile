using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMunging.Reporting.Reports
{
    public class MinMaxReport
    {
        private readonly IEnumerable<Tuple<int, int>> points;

        private IList<int> differences;

        public MinMaxReport(IEnumerable<Tuple<int, int>> points)
        {
            this.points = points;
        }

        public IList<int> Differences
        {
            get => differences ?? (differences = GetDifferences());
            set => differences = value;
        }

        public int GetMaxSpread()
        {
            return Differences.LastOrDefault();
        }

        public int GetMinSpread()
        {
            return Differences.FirstOrDefault();
        }

        private IList<int> GetDifferences()
        {
            return points
                .Select(x => Math.Abs(x.Item1 - x.Item2))
                .OrderBy(x => x)
                .ToList();
        }
    }
}