using BLL.Clients;
using BLL.Extensions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Newtonsoft.Json;

namespace BLL.Services
{
    public class FixtureService: IFixtureService
    {

        private readonly FootballClient _client;
        private readonly ITeamStatisticsService _teamStatisticService;
        private readonly IDynamoDBClient _dynamoDbClient;
        public FixtureService(FootballClient footballClient, ITeamStatisticsService teamStatisticService, IDynamoDBClient dynamoDBClient)
        {
            _client = footballClient;
            _dynamoDbClient = dynamoDBClient;
            _teamStatisticService = teamStatisticService;
        }


        public async Task<Fixture> GetFixtureUpcomingByTeam(int team)
        {
            var response = await _client._httpclient.GetAsync($"/v3/fixtures?team={team}&next=5");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Fixture>(content);

            if (result.Response.Count != 0)
            {
                foreach (var fixture in result.Response)
                {
                    var odds = GetOdds(fixture.Fixture.Id.HasValue ? fixture.Fixture.Id.Value : 0);
                    var OddsList = new List<Odds>();
                    fixture.Odds = OddsList;
                    if (odds != null)
                    {
                        fixture.Odds.Add(odds.Result);
                    }

                }

                return result;
            }
            else { return null; }
        }

        public async Task<Fixture> GetFixtureUpcomingByTeam(string team)
        {
            var teamInfoDB = await _dynamoDbClient.GetTeamByName(team);
            int teamId;
            if (teamInfoDB != null)
            {
                teamId = teamInfoDB.Item.ToClass<TeamInfoDb>().Id;
            }
            else
            {
                var teamInfoApi = await _teamStatisticService.GetTeamInfo(team);
                if (teamInfoApi != null)
                {
                    teamId = teamInfoApi.Response.FirstOrDefault().Team.Id;

                    var data = new TeamInfoDb
                    {
                        Id = teamInfoApi.Response.FirstOrDefault().Team.Id,
                        Name = teamInfoApi.Response.FirstOrDefault().Team.Name,
                        Country = teamInfoApi.Response.FirstOrDefault().Team.Country

                    };

                    _dynamoDbClient.PostTeam(data);
                }
                else { return null; }

            }
            return await GetFixtureUpcomingByTeam(teamId);
        }

        public async Task<Fixture> GetFixtureLastByTeam(int team)
        {
            var response = await _client._httpclient.GetAsync($"/v3/fixtures?team={team}&last=5");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Fixture>(content);
            if (result.Response.Count != 0) { return result; }
            else { return null; }
        }

        public async Task<Fixture> GetFixtureLastByTeam(string team)
        {
            var teamInfoDB = await _dynamoDbClient.GetTeamByName(team);
            int teamId;
            if (teamInfoDB != null)
            {
                teamId = teamInfoDB.Item.ToClass<TeamInfoDb>().Id;
            }
            else
            {
                var teamInfoApi = await _teamStatisticService.GetTeamInfo(team);
                if (teamInfoApi != null)
                {
                    teamId = teamInfoApi.Response.FirstOrDefault().Team.Id;

                    var data = new TeamInfoDb
                    {
                        Id = teamInfoApi.Response.FirstOrDefault().Team.Id,
                        Name = teamInfoApi.Response.FirstOrDefault().Team.Name,
                        Country = teamInfoApi.Response.FirstOrDefault().Team.Country

                    };

                    _dynamoDbClient.PostTeam(data);
                }
                else { return null; }

            }
            return await GetFixtureLastByTeam(teamId);
        }

        public async Task<Odds> GetOdds(int fixture)
        {
            var response = await _client._httpclient.GetAsync($"/v3/odds?fixture={fixture}&bookmaker=11&bet=1");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Odds>(content);

            if (result.Response.Count != 0)
            {
                return result;
            }
            else { return null; }
        }

    }
}
