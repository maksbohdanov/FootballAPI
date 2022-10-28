using BLL.Responses;
using DAL.Entities;

namespace BLL.Extensions
{
    public static class FixtureDbConverter
    {
        public static FixtureResponse ConvertFromDb(this FixtureDb fixtureDb)
        {
            return new FixtureResponse()
            {

                Id = fixtureDb.Id,
                IdChat = fixtureDb.IdChat,
                IdFixture = fixtureDb.IdFixture,
                Date = fixtureDb.Date,
                Stadium = fixtureDb.Stadium,
                City = fixtureDb.City,
                Status = fixtureDb.Status,
                League = new LeagueFixtureResponse
                {
                    Id = fixtureDb.IdLeague,
                    Name = fixtureDb.League,
                    Country = fixtureDb.Country,
                    Season = fixtureDb.Season
                },
                Teams = new TeamFixtureResponse
                {
                    IdHome = fixtureDb.IdHome,
                    NameHome = fixtureDb.NameHome,
                    GoalHome = fixtureDb.GoalHome != "no data" ? Int32.Parse(fixtureDb.GoalHome) : null,


                    IdAway = fixtureDb.IdAway,
                    NameAway = fixtureDb.NameAway,
                    GoalAway = fixtureDb.GoalAway != "no data" ? Int32.Parse(fixtureDb.GoalAway) : null
                },
                Odds = new OddsResponse
                {
                    Home = fixtureDb.Home,
                    Draw = fixtureDb.Draw,
                    Away = fixtureDb.Away

                }

            };
        }

        public static  FixtureDb ConvertFromDb(this FixtureResponse fixtureDb)
        {
            return new FixtureDb()
            {
                IdFixture = fixtureDb.IdFixture,
                IdChat = fixtureDb.IdChat,
                Date = fixtureDb?.Date ?? null,
                Stadium = fixtureDb?.Stadium ?? null,
                City = fixtureDb?.City ?? null,
                Status = fixtureDb.Status,
                IdLeague = fixtureDb.League.Id,
                League = fixtureDb.League.Name,
                Country = fixtureDb?.League?.Country ?? null,
                Season = fixtureDb?.League?.Season ?? null,

                Home = fixtureDb?.Odds?.Home ?? null,
                Draw = fixtureDb?.Odds?.Draw ?? null,
                Away = fixtureDb?.Odds?.Away ?? null,

                IdHome = fixtureDb.Teams.IdHome,
                NameHome = fixtureDb.Teams.NameHome,
                GoalHome = fixtureDb.Teams.GoalHome != null ? fixtureDb.Teams.GoalHome.ToString() : null,


                IdAway = fixtureDb.Teams.IdAway,
                NameAway = fixtureDb.Teams.NameAway,
                GoalAway = fixtureDb.Teams.GoalAway != null ? fixtureDb.Teams.GoalAway.ToString() : null
            };
        }
    }
}
