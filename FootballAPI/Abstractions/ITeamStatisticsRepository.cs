using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballAPI.Models.Data;

namespace FootballAPI.Abstractions
{
    public interface ITeamStatisticsRepository
    {
        Task<TeamStatictics> GetTeamStatictics(int team, int league);
        Task<TeamStatictics> GetTeamStaticticsByName(string team, string league);

        Task<Standings> GetLeagueStandings(int league);
        Task<Standings> GetLeagueStandingsByName(string league, string country);

        Task<TeamInfo> GetTeamInfo(string name);
        Task<League> GetLeague(string league, int team);
        Task<League> GetLeague(string league, string country);


    }
}
