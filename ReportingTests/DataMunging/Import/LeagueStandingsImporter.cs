using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataMunging.Reporting.TestForSkip;
using DataMunging.Reporting.TypeConverters;
using DataMunging.Reporting.ViewModels;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;

namespace DataMunging.Reporting.Import
{
    public class LeagueStandingsImporter : FlatFileImporter<LeagueScore>
    {
        public LeagueStandingsImporter(string fileName) : base(fileName)
        {
        }

        public List<LeagueScore> GetScoreSpreads()
        {
            return GetRows().ToList();
        }

        public override IFlatFileLayoutDescriptor<LeagueScore> GetLayout(string fileName)
        {
            var layout = new LayoutDescriptor<LeagueScore>()
                .AppendIgnoredField(7) // Id
                .AppendField(x => x.TeamName, 16)
                .AppendIgnoredField(20)
                .AppendField(x => x.GoalsFor, 4)
                .AppendIgnoredField(3)
                .AppendField(x => x.GoalsAgainst, 6)
                .WithSkipDefinition(new LeagueStandingsSkipDefinitions());
            return layout;
        }
    }
}