using System.Collections.ObjectModel;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.UnitTests
{
    public static class MockedData
    {
        public static Collection<LeagueScore> GetExpectedScores()
        {
            // Mocked expected result based on ~\DataMunging\UnitTests\InputFiles\football.dat
            return new Collection<LeagueScore>
            {
                new LeagueScore("Arsenal", 79, 36),
                new LeagueScore("Liverpool", 67, 30),
                new LeagueScore("Manchester_U", 87, 45),
                new LeagueScore("Newcastle", 74, 52),
                new LeagueScore("Leeds", 53, 37),
                new LeagueScore("Chelsea", 66, 38),
                new LeagueScore("West_Ham", 48, 57),
                new LeagueScore("Aston_Villa", 46, 47),
                new LeagueScore("Tottenham", 49, 53),
                new LeagueScore("Blackburn", 55, 51),
                new LeagueScore("Southampton", 46, 54),
                new LeagueScore("Middlesbrough", 35, 47),
                new LeagueScore("Fulham", 36, 44),
                new LeagueScore("Charlton", 38, 49),
                new LeagueScore("Everton", 45, 57),
                new LeagueScore("Bolton", 44, 62),
                new LeagueScore("Sunderland", 29, 51),
                new LeagueScore("Ipswich", 41, 64),
                new LeagueScore("Derby", 33, 63),
                new LeagueScore("Leicester", 30, 64)
            };
        }

        public static Collection<DailyTemperature> GetExpectedTemperatures()
        {
            // Mocked expected result based on ~\DataMunging\UnitTests\InputFiles\weather.dat
            return new Collection<DailyTemperature>
            {
                new DailyTemperature(1, 88, 59),
                new DailyTemperature(2, 79, 63),
                new DailyTemperature(3, 77, 55),
                new DailyTemperature(4, 77, 59),
                new DailyTemperature(5, 90, 66),
                new DailyTemperature(6, 81, 61),
                new DailyTemperature(7, 73, 57),
                new DailyTemperature(8, 75, 54),
                new DailyTemperature(9, 86, 32),
                new DailyTemperature(10, 84, 64),
                new DailyTemperature(11, 91, 59),
                new DailyTemperature(12, 88, 73),
                new DailyTemperature(13, 70, 59),
                new DailyTemperature(14, 61, 59),
                new DailyTemperature(15, 64, 55),
                new DailyTemperature(16, 79, 59),
                new DailyTemperature(17, 81, 57),
                new DailyTemperature(18, 82, 52),
                new DailyTemperature(19, 81, 61),
                new DailyTemperature(20, 84, 57),
                new DailyTemperature(21, 86, 59),
                new DailyTemperature(22, 90, 64),
                new DailyTemperature(23, 90, 68),
                new DailyTemperature(24, 90, 77),
                new DailyTemperature(25, 90, 72),
                new DailyTemperature(26, 97, 64),
                new DailyTemperature(27, 91, 72),
                new DailyTemperature(28, 84, 68),
                new DailyTemperature(29, 88, 66),
                new DailyTemperature(30, 90, 45)
            };
        }
    }
}