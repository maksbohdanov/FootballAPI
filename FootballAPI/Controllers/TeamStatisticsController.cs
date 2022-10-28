using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Extensions;
using BLL.Responses;

namespace FootballAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamStatisticsController : ControllerBase
    {
        private readonly ITeamStatisticsService _teamStatisticsService;

        public TeamStatisticsController(ITeamStatisticsService teamStatisticsService)
        {
            _teamStatisticsService = teamStatisticsService;
        }


        [HttpGet("statName")]
        public async Task<TeamStatisticsResponse> TeamStaticticsByName([FromQuery] string team, string league)
        {
            var teamStat = await _teamStatisticsService.GetTeamStaticticsByName(team, league);
            if (teamStat != null)
            {
                return teamStat.Convert();
            }
            else { return null; }            
        }


        [HttpGet("standings")]
        public async Task<StandingsResponse> StandingsByName([FromQuery] string league, string country)
        {
            try
            {
                var leagueStand = await _teamStatisticsService.GetLeagueStandingsByName(league, country);
                var statList = new List<Stat>();
                if (leagueStand != null)
                {
                    return leagueStand.Convert();   
                }
                else { return null; }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Here is your exception\n{e.Message}");
                return null;
            }         
        }


        [HttpGet("Teaminfo")]
        public async Task<TeamInfoResponse> TeamInfo([FromQuery] string name)
        {
            var team = await _teamStatisticsService.GetTeamInfo(name);

            if (team != null)
            {
                var result = new TeamInfoResponse
                {
                    Id = team.Response.FirstOrDefault().Team.Id,
                    Name = team.Response.FirstOrDefault().Team.Name,
                    Country = team.Response.FirstOrDefault().Team.Country,
                };
                return result;
            }
            else { return null; }            
        }       
    }
}
