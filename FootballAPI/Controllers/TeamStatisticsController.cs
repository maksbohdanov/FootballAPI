using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballAPI.Abstractions;
using FootballAPI.Models.Data;
using FootballAPI.Models.Responses;


namespace FootballAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamStatisticsController : ControllerBase
    {
        private readonly ITeamStatisticsRepository _teamStatisticsRepository;

        public TeamStatisticsController(ITeamStatisticsRepository teamStatisticsRepository)
        {
            _teamStatisticsRepository = teamStatisticsRepository;
        }


        [HttpGet("statName")]
        public async Task<TeamStatisticsResponse> TeamStaticticsByName([FromQuery] string team, string league)
        {
            var teamStat = await _teamStatisticsRepository.GetTeamStaticticsByName(team, league);
            if (teamStat != null)
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
            else { return null; }

            
        }


        [HttpGet("standings")]
        public async Task<StandingsResponse> StandingsByName([FromQuery] string league, string country)
        {
            try
            {
                var leagueStand = await _teamStatisticsRepository.GetLeagueStandingsByName(league, country);
                var statList = new List<Stat>();
                if (leagueStand != null)
                {
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

                    if (leagueStand != null)
                    {
                        var result = new StandingsResponse
                        {
                            League = leagueStand.Response.FirstOrDefault().League.Name,
                            Country = leagueStand.Response.FirstOrDefault().League.Country,
                            Season = leagueStand.Response.FirstOrDefault().League.Season,
                            TeamsStat = statList
                        };

                        return result;
                    }
                    else { return null; }
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
        public async Task<TeamInfoDBResponse> TeamInfo([FromQuery] string name)
        {
            var team = await _teamStatisticsRepository.GetTeamInfo(name);

            if (team != null)
            {
                var result = new TeamInfoDBResponse
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
