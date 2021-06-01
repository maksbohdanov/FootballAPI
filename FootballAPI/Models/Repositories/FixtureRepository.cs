using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballAPI.Abstractions;
using FootballAPI.Clients;
using FootballAPI.Models.Data;
using FootballAPI.Models.Responses;
using Newtonsoft.Json;

namespace FootballAPI.Models.Repositories
{
    public class FixtureRepository: IFixtureRepository
    {
        private readonly FootballClient _client;
        private readonly ITeamStatisticsRepository _teamStatisticRepository;
        private readonly IDynamoDBClient _dynamoDbClient;
        public FixtureRepository(FootballClient footballClient, ITeamStatisticsRepository teamStatisticRepository, IDynamoDBClient dynamoDBClient)
        {
            _client = footballClient;
            _dynamoDbClient = dynamoDBClient;
            _teamStatisticRepository = teamStatisticRepository;
        }

        /////////////////////////////////////////////////////        

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
                teamId = teamInfoDB.Id;
            }
            else
            {
                var teamInfoApi = await _teamStatisticRepository.GetTeamInfo(team);
                if (teamInfoApi != null)
                {
                    teamId = teamInfoApi.Response.FirstOrDefault().Team.Id;

                    var data = new TeamInfoDBResponse
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
            if (result.Response.Count != 0)     { return result; }
            else    { return null; }

            
        }       
        public async Task<Fixture> GetFixtureLastByTeam(string team)
        {
            var teamInfoDB = await _dynamoDbClient.GetTeamByName(team);
            int teamId;
            if (teamInfoDB != null)
            {
                teamId = teamInfoDB.Id;
            }
            else
            {
                var teamInfoApi = await _teamStatisticRepository.GetTeamInfo(team);
                if (teamInfoApi != null)
                {
                    teamId = teamInfoApi.Response.FirstOrDefault().Team.Id;

                    var data = new TeamInfoDBResponse
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
