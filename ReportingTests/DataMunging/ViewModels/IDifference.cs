namespace DataMunging.Reporting.ViewModels
{
    public interface IDailyTemperatures
    {
        int DayId { get; set; }
        int LowTemperature { get; set; }
        int HighTemperature { get; set; }

        bool Equals(object obj);
        int GetHashCode();
    }
}