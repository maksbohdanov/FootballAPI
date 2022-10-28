using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BLL.Interfaces;
using BLL.Responses;
using BLL.Extensions;

namespace FootballAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixtureController : ControllerBase
    {
        private readonly IFixtureService _fixtureService;


        public FixtureController(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }

        [HttpGet("teamUpcoming")]
        public async Task<List<FixtureResponse>> FixtureUpcomingByTeam([FromQuery] string team)
        {
            var fixtures = await _fixtureService.GetFixtureUpcomingByTeam(team);
            var result = new List<FixtureResponse>();
            if (fixtures != null)
            {
                foreach (var item in fixtures.Response)
                {                  
                    result.Add(item.Convert());
                }
                return result;
            }
            else { return null; }
        }

        [HttpGet("teamLast")]
        public async Task<List<FixtureResponse>> FixtureLastByTeam([FromQuery] string team)
        {
            var fixtures = await _fixtureService.GetFixtureLastByTeam(team);
            var result = new List<FixtureResponse>();
            if (fixtures != null)
            {
                foreach (var item in fixtures.Response)
                {
                    result.Add(item.Convert());
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
            var result = await _fixtureService.GetFavoriteFixtures(user);
            if(result == null)
                return NotFound("There are no records in db");

            return Ok(result);
        }

        //POST
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToFavorites([FromBody] FixtureResponse fixture)
        {
            var result = await _fixtureService.AddToFavorites(fixture);

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
            var result = await _fixtureService.DeleteFromFavorites(id);
            if (result == false)
            {
                return BadRequest("Cannot delete value from database. Please see console log");
            }

            return Ok("Value has been successfully deleted from DB");
        }
    }
}
