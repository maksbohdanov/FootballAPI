using BLL.Models;
using BLL.Responses;

namespace BLL.Extensions
{
    public static class TeamStatisticsConverter
    {
        public static TeamStatisticsResponse Convert(this TeamStatictics teamStat)
        {
            var result = new TeamStatisticsResponse
            {
                Id = teamStat.Response.Team.Id,
                Team = teamStat.Response.Team.Name,
                LeagueId = teamStat.Response.League.Id,
                League = teamStat.Response.League.Name,
                Season = teamStat.Response.League.Season,
                Games = new GamesTeamStatResp
                {
                    Total = teamStat.Response.Fixtures.Played.Total,
                    Wins = teamStat.Response.Fixtures.Wins.Total,
                    Loses = teamStat.Response.Fixtures.Loses.Total,
                    Draws = teamStat.Response.Fixtures.Draws.Total,
                    CleanSheet = teamStat.Response.CleanSheet?.Total

                },
                Goals = new GoalsTeamStatResp
                {
                    TotalFor = teamStat.Response.Goals.For.Total.Total,
                    AverageFor = teamStat.Response.Goals.For.Average.Total,
                    TotalAgainst = teamStat.Response.Goals.Against.Total.Total,
                    AverageAgainst = teamStat.Response.Goals.Against.Average.Total,
                },
                Penalties = new PenaltyTeamStatResp
                {
                    Total = teamStat.Response.Penalty?.Total,
                    Scored = teamStat.Response.Penalty?.Scored.Total,
                    Missed = teamStat.Response.Penalty?.Missed.TotalResult
                }
            };

            return result;
        }
    }
}
