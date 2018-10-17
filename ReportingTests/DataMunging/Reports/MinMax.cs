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
        private IList<int> _differences;
        private IList<int> differences =>
            _differences ?? (_differences = GetDifferences());

        public ReportMinMax(IEnumerable<IPoint> points)
        {
            this.points = points;
        }
        
        public int GetMaxSpread(IEnumerable<IPoint> points)
        {
            return differences.FirstOrDefault();
        }

        public int GetMinSpread(IEnumerable<IPoint> points)
        {
            return differences.LastOrDefault();
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