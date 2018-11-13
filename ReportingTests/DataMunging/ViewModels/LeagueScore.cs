using System.Collections.Generic;

namespace DataMunging.Reporting.ViewModels
{
    public class LeagueScore : ILeagueScore
    {
        public LeagueScore(string teamName, int goalsFor, int goalsAgainst)
        {
            TeamName = teamName;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
        }

        // Parameterless constructor is required so model can be newed up when used as a generic type (e.g. in FlatFileImporter<T>)
        public LeagueScore()
        {
        }

        public string TeamName { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        public override bool Equals(object obj)
        {
            return obj is LeagueScore score &&
                   TeamName == score.TeamName &&
                   GoalsFor == score.GoalsFor &&
                   GoalsAgainst == score.GoalsAgainst;
        }

        public override int GetHashCode()
        {
            var hashCode = 1380533899;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TeamName);
            hashCode = hashCode * -1521134295 + GoalsFor.GetHashCode();
            hashCode = hashCode * -1521134295 + GoalsAgainst.GetHashCode();
            return hashCode;
        }
    }
}