using System;
using System.Collections.Generic;
using System.Linq;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Reporting.Reports
{
    // Given a collection of Id, Min, and Max values, report min and max spreads
    public class ReportMinMax
    {
        private readonly IEnumerable<IPoint> points;

        public ReportMinMax(IEnumerable<IPoint> points)
        {
            this.points = points;
        }

        private IList<int> differences
        {
            get => differences ?? (differences = GetDifferences());
            set { }
        }

        public int GetMaxSpread()
        {
            return differences.LastOrDefault();
        }

        public int GetMinSpread()
        {
            return differences.FirstOrDefault();
        }

        private IList<int> GetDifferences()
        {
            return points
                .Select(x => Math.Abs(x.X - x.Y))
                .OrderBy(x => x)
                .ToList();
        }
    }
}