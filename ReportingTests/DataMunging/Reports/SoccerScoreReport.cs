using System;
using System.Collections.Generic;
using System.Linq;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Reporting.Reports
{
    public class SoccerScoreReport<T> where T : ILeagueScore
    {
        private readonly MinMaxReport minMaxReport;

        public SoccerScoreReport(IEnumerable<T> points)
        {
            var tuples = points.Select(x => new Tuple<int, int>(x.GoalsFor, x.GoalsAgainst));
            minMaxReport = new MinMaxReport(tuples);
        }

        public int GetLargestPointSpread()
        {
            return minMaxReport.GetMaxSpread();
        }

        public int GetSmallestPointSpread()
        {
            return minMaxReport.GetMinSpread();
        }
    }
}