using FootballAPI.Abstractions;
using FootballAPI.Clients;
using FootballAPI.Models;
using FootballAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballAPI.Models.Responses;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using FootballAPI.Extensions;
using Microsoft.AspNetCore.Http;

namespace FootballAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixtureController : ControllerBase
    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IDynamoDBClient _dynamoDbClient;


        public FixtureController(IFixtureRepository fixtureRepository, IDynamoDBClient dynamoDBClient)
        {
            _fixtureRepository = fixtureRepository;
            _dynamoDbClient = dynamoDBClient;
        }

        [HttpGet("teamUpcoming")]
        public async Task<List<FixtureResponse>> FixtureUpcomingByTeam([FromQuery] string team)
        {
            var fixtures = await _fixtureRepository.GetFixtureUpcomingByTeam(team);
            var result = new List<FixtureResponse>();
            if (fixtures != null)
            {
                foreach (var item in fixtures.Response)
                {
                    var toAdd = new FixtureResponse
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
                    result.Add(toAdd);
                }
                return result;
            }
            else { return null; }

        }

        [HttpGet("teamLast")]
        public async Task<List<FixtureResponse>> FixtureLastByTeam([FromQuery] string team)
        {
            var fixtures = await _fixtureRepository.GetFixtureLastByTeam(team);
            var result = new List<FixtureResponse>();
            if (fixtures != null)
            {
                foreach (var item in fixtures.Response)
                {
                    var toAdd = new FixtureResponse
                    {
                        IdFixture = item.Fixture.Id,
                        Date = item.Fixture?.Date.ToString(),
                        Stadium = item.Fixture?.Venue?.Name,
                        City = item.Fixture?.Venue?.City,
                        Status = item.Fixture?.Status?.Long,
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
                            GoalHome = item.Goals?.Home,

                            IdAway = item.Teams.Away.Id,
                            NameAway = item.Teams.Away.Name,
                            GoalAway = item.Goals?.Away,

                        },
                    };
                    result.Add(toAdd);
                }
                return result;
            }
            else { return null; }

        }       


        [HttpGet("all_favorites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] string user)
        {
            var response = await _dynamoDbClient.GetAll(user);

            if (response == null)
                return NotFound("There are no records in db");

            var result = response
                .Select(result => new FixtureResponse()
                {
                    Id = result.Id,
                    IdChat = result.IdChat,
                    IdFixture = result.IdFixture,
                    Date = result.Date ,
                    Stadium = result.Stadium ,
                    City = result.City ,
                    Status = result.Status,
                    League = new LeagueFixtureResponse
                    {
                        Id = result.IdLeague,
                        Name = result.League,
                        Country = result.Country    ,
                        Season = result.Season
                    },
                    Teams = new TeamFixtureResponse
                    {
                        IdHome = result.IdHome,
                        NameHome = result.NameHome,
                        GoalHome = result.GoalHome !="no data" ? Int32.Parse(result.GoalHome) : null,


                        IdAway = result.IdAway,
                        NameAway = result.NameAway,
                        GoalAway = result.GoalAway != "no data" ? Int32.Parse(result.GoalAway) : null
                    },
                    Odds = new OddsResponse
                    {
                        Home = result.Home,
                        Draw = result.Draw,
                        Away = result.Away

                    }
                })
                .ToList();

            return Ok(result);


        }

        //POST
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToFavorites([FromBody] FixtureResponse fixture)
        {
            var data = new FixtureDBResponse
            {
                IdFixture = fixture.IdFixture,
                IdChat = fixture.IdChat,
                Date = fixture?.Date ?? null,
                Stadium = fixture?.Stadium ?? null,
                City = fixture?.City ?? null,
                Status = fixture.Status,
                IdLeague = fixture.League.Id,
                League = fixture.League.Name,
                Country = fixture?.League?.Country ?? null,
                Season = fixture?.League?.Season ?? null,

                Home = fixture?.Odds?.Home ?? null,
                Draw = fixture?.Odds?.Draw ?? null,
                Away = fixture?.Odds?.Away ?? null,

                IdHome = fixture.Teams.IdHome,
                NameHome = fixture.Teams.NameHome,
                GoalHome = fixture.Teams.GoalHome != null ? fixture.Teams.GoalHome.ToString() : null,


                IdAway = fixture.Teams.IdAway,
                NameAway = fixture.Teams.NameAway,
                GoalAway = fixture.Teams.GoalAway != null ? fixture.Teams.GoalAway.ToString() : null
            };

            var result = await _dynamoDbClient.PostFixture(data);

            if (result == false)
            {
                return BadRequest("Cannot insert value to database. Please see console log");
            }

            return Ok("Value has been successfully added to DB");
        }       


        //DELETE
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFromFavorites([FromQuery] string id)
        {
            var result = await _dynamoDbClient.Delete(id);
            if (result == false)
            {
                return BadRequest("Cannot delete value from database. Please see console log");
            }

            return Ok("Value has been successfully deleted from DB");
        }



    }
}
