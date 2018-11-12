namespace DataMunging.Reporting.ViewModels
{
    public interface ILeagueScore
    {
        int GoalsAgainst { get; set; }
        int GoalsFor { get; set; }
        string TeamName { get; set; }

        bool Equals(object obj);
        int GetHashCode();
    }
}