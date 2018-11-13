namespace DataMunging.Reporting.ViewModels
{
    public interface IDailyTemperature
    {
        int DayId { get; set; }
        int LowTemperature { get; set; }
        int HighTemperature { get; set; }

        bool Equals(object obj);
        int GetHashCode();
    }
}