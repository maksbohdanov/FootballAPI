using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballAPI.Models.Data;


namespace FootballAPI.Abstractions
{
    public interface IFixtureRepository
    {
        Task<Fixture> GetFixtureUpcomingByTeam(int team);
        Task<Fixture> GetFixtureUpcomingByTeam(string team);
        Task<Fixture> GetFixtureLastByTeam(int team);
        Task<Fixture> GetFixtureLastByTeam(string team);
        Task<Odds> GetOdds(int fixture);
    }
}
