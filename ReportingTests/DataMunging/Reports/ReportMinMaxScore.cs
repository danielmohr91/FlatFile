﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Reporting.Reports
{
    // Given a collection of DayDayId, Min, and Max values, report min and max spreads
    public class ReportMinMaxScore<T> where T : ILeagueScore
    {
        private readonly IEnumerable<T> points;

        public ReportMinMaxScore(IEnumerable<T> points)
        {
            this.points = points;
        }

        private IList<int> differences;

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
                .Select(x => Math.Abs(x.GoalsAgainst - x.GoalsFor))
                .OrderBy(x => x)
                .ToList();
        }
    }
}