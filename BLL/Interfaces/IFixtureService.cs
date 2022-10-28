using BLL.Models;
using BLL.Responses;

namespace BLL.Interfaces
{
    public interface IFixtureService
    {
        Task<Fixture> GetFixtureUpcomingByTeam(int team);
        Task<Fixture> GetFixtureUpcomingByTeam(string team);
        Task<Fixture> GetFixtureLastByTeam(int team);
        Task<Fixture> GetFixtureLastByTeam(string team);
        Task<Odds> GetOdds(int fixture);
        Task<ICollection<FixtureResponse>> GetFavoriteFixtures(string user);
        Task<bool> AddToFavorites(FixtureResponse fixture);
        Task<bool> DeleteFromFavorites(string id);
    }
}
