using BLL.Models;

namespace BLL.Interfaces
{
    public interface IFixtureService
    {
        Task<Fixture> GetFixtureUpcomingByTeam(int team);
        Task<Fixture> GetFixtureUpcomingByTeam(string team);
        Task<Fixture> GetFixtureLastByTeam(int team);
        Task<Fixture> GetFixtureLastByTeam(string team);
        Task<Odds> GetOdds(int fixture);
    }
}
