using System;
using System.Collections.Generic;
using System.Linq;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Reporting.Reports
{
    public class SoccerScoreReport<T> where T : ILeagueScore
    {
        private readonly IEnumerable<T> scores;
        private IList<int> differences;

        public SoccerScoreReport(IEnumerable<T> scores)
        {
            this.scores = scores;
        }

        public IList<int> Differences
        {
            get => differences ?? (differences = GetDifferences());
            set => differences = value;
        }

        public int GetLargestPointSpread()
        {
            return Differences.LastOrDefault();
        }

        public int GetSmallestPointSpread()
        {
            return Differences.FirstOrDefault();
        }

        private IList<int> GetDifferences()
        {
            return scores
                .Select(x => Math.Abs(x.GoalsFor - x.GoalsAgainst))
                .OrderBy(x => x)
                .ToList();
        }
    }
}