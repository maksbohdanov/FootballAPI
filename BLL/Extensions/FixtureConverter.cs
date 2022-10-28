using BLL.Models;
using BLL.Responses;

namespace BLL.Extensions
{
    public static class FixtureConverter
    {
        public static FixtureResponse Convert(this ResponseFixure item)
        {
            var result = new FixtureResponse
            {
                IdFixture = item.Fixture.Id,
                Date = item.Fixture?.Date.ToString(),
                Stadium = item.Fixture?.Venue?.Name ?? null,
                City = item.Fixture?.Venue?.City ?? null,
                Status = item.Fixture?.Status?.Long ?? null,
                League = new LeagueFixtureResponse
                {
                    Id = item.League.Id,
                    Name = item.League.Name,
                    Country = item.League?.Country,
                    Season = item.League?.Season,
                },
                Teams = new TeamFixtureResponse
                {
                    IdHome = item.Teams.Home.Id,
                    NameHome = item.Teams.Home.Name,
                    GoalHome = item.Goals?.Home ?? null,

                    IdAway = item.Teams.Away.Id,
                    NameAway = item.Teams.Away.Name,
                    GoalAway = item.Goals?.Away ?? null,

                },
                Odds = new OddsResponse
                {
                    Home = item.Odds.FirstOrDefault()?.Response.FirstOrDefault().Bookmakers.FirstOrDefault().Bets.FirstOrDefault().Values[0].Odd ?? null,
                    Draw = item.Odds.FirstOrDefault()?.Response.FirstOrDefault().Bookmakers.FirstOrDefault().Bets.FirstOrDefault().Values[1].Odd ?? null,
                    Away = item.Odds.FirstOrDefault()?.Response.FirstOrDefault().Bookmakers.FirstOrDefault().Bets.FirstOrDefault().Values[2].Odd ?? null,
                },
            };
            return result;
        }
    }
}
