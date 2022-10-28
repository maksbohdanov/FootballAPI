using BLL.Models;
using BLL.Responses;

namespace BLL.Extensions
{
    public static class StandingsConverter
    {
        public static StandingsResponse Convert(this Standings leagueStand)
        {
            var statList = new List<Stat>();
            foreach (var item in leagueStand.Response.FirstOrDefault().League.Standings.FirstOrDefault())
            {
                var toAdd = new Stat
                {
                    Id = item.Team.Id,
                    Team = item.Team.Name,
                    Rank = item.Rank,
                    Points = item.Points,
                    Played = item.All.Played,
                    Win = item.All.Win,
                    Draw = item.All.Draw,
                    Lose = item.All.Lose,
                    GoalsFor = item.All.Goals.For,
                    GoalsAgainst = item.All.Goals.Against,
                    GoalsDiff = item.GoalsDiff
                };
                statList.Add(toAdd);
            }

            var result = new StandingsResponse
            {
                League = leagueStand.Response.FirstOrDefault().League.Name,
                Country = leagueStand.Response.FirstOrDefault().League.Country,
                Season = leagueStand.Response.FirstOrDefault().League.Season,
                TeamsStat = statList
            };

            return result;
        }
    }
}
