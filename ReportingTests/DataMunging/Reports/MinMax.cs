using System;
using System.Collections.Generic;
using System.Linq;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Reporting.Reports
{
    // Given a collection of DayDayId, Min, and Max values, report min and max spreads
    public class ReportMinMax
    {
        private readonly IEnumerable<IDailyTemperatures> points;

        public ReportMinMax(IEnumerable<IDailyTemperatures> points)
        {
            this.points = points;
        }

        // Infinite Recursive Loop... fix this
        // private IList<int> differences
        //{
        //    get => differences ?? (differences = GetDifferences());
        //    set { }
        //}

        // Infinite Recursive Loop... fix this
        //private IList<int> differences
        //{
        //    get { return differences ?? (differences = GetDifferences()); }
        //    set { differences = value; }
        //}

        private IList<int> _differences;

        public IList<int> differences
        {
            get { return _differences ?? (_differences = GetDifferences()); }
            set { _differences = value; }
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
                .Select(x => Math.Abs(x.LowTemperature - x.HighTemperature))
                .OrderBy(x => x)
                .ToList();
        }
    }
}