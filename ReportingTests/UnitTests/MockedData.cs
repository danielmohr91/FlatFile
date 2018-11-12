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

        public static Collection<DailyTemperatures> GetExpectedTemperatures()
        {
            // Mocked expected result based on ~\DataMunging\UnitTests\InputFiles\weather.dat
            return new Collection<DailyTemperatures>
            {
                new DailyTemperatures(1, 88, 59),
                new DailyTemperatures(2, 79, 63),
                new DailyTemperatures(3, 77, 55),
                new DailyTemperatures(4, 77, 59),
                new DailyTemperatures(5, 90, 66),
                new DailyTemperatures(6, 81, 61),
                new DailyTemperatures(7, 73, 57),
                new DailyTemperatures(8, 75, 54),
                new DailyTemperatures(9, 86, 32),
                new DailyTemperatures(10, 84, 64),
                new DailyTemperatures(11, 91, 59),
                new DailyTemperatures(12, 88, 73),
                new DailyTemperatures(13, 70, 59),
                new DailyTemperatures(14, 61, 59),
                new DailyTemperatures(15, 64, 55),
                new DailyTemperatures(16, 79, 59),
                new DailyTemperatures(17, 81, 57),
                new DailyTemperatures(18, 82, 52),
                new DailyTemperatures(19, 81, 61),
                new DailyTemperatures(20, 84, 57),
                new DailyTemperatures(21, 86, 59),
                new DailyTemperatures(22, 90, 64),
                new DailyTemperatures(23, 90, 68),
                new DailyTemperatures(24, 90, 77),
                new DailyTemperatures(25, 90, 72),
                new DailyTemperatures(26, 97, 64),
                new DailyTemperatures(27, 91, 72),
                new DailyTemperatures(28, 84, 68),
                new DailyTemperatures(29, 88, 66),
                new DailyTemperatures(30, 90, 45)
            };
        }
    }
}