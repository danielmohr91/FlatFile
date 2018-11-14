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
            minMaxReport = new MinMaxReport(points.Select(x => new Tuple<int, int>(x.GoalsFor, x.GoalsAgainst)));
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