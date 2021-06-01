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
    public class TeamStatisticRepository : ITeamStatisticsRepository
    {
        private readonly FootballClient _client;
        private readonly IDynamoDBClient _dynamoDbClient;
        public TeamStatisticRepository(FootballClient footballClient, IDynamoDBClient dynamoDBClient)
        {
            _client = footballClient;
            _dynamoDbClient = dynamoDBClient;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<TeamStatictics> GetTeamStatictics(int team, int league)
        {            
            var response = await _client._httpclient.GetAsync($"/v3/teams/statistics?team={team}&league={league}&season=2020");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            try
            {
                var result = JsonConvert.DeserializeObject<TeamStatictics>(content);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is your error \n" + e);
                return null;

            }


        }
        public async Task<TeamStatictics> GetTeamStaticticsByName(string team, string league)
        {
            var teamInfoDB = await _dynamoDbClient.GetTeamByName(team);
            int teamId;
            if (teamInfoDB != null)
            {
                teamId = teamInfoDB.Id;
            }
            else
            {                
                var teamInfoApi = await GetTeamInfo(team);
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

            var leagueinfo = await GetLeague(league, teamId);
            
            if (leagueinfo != null)
            {
                 var leagueId = leagueinfo.Response.FirstOrDefault().League.Id;
                return await GetTeamStatictics(teamId, leagueId);
            }
            else { return null; }            
        }

        public async Task<Standings> GetLeagueStandings(int league)
        {
            var response = await _client._httpclient.GetAsync($"/v3/standings?league={league}&season=2020");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            try
            {
                var result = JsonConvert.DeserializeObject<Standings>(content);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is your error \n" + e);
                return null;

            }
        }
        public async Task<Standings> GetLeagueStandingsByName(string league, string country)
        {
            if (country.ToLower() == "world")
            {
                return null;
            }  
            var leagueinfo = await GetLeague(league, country);

            if (leagueinfo != null)
            {
                var leagueId = leagueinfo.Response.FirstOrDefault().League.Id;
                return await GetLeagueStandings(leagueId);
            }
            else { return null; }
        }

        public async Task<TeamInfo> GetTeamInfo(string name)
        {
            var response = await _client._httpclient.GetAsync($"/v3/teams?name={name}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<TeamInfo>(content);

            if (result.Response.Count != 0)
            {
                return result;
            }
            else { return null; }

            
        }

        public async Task<League> GetLeague(string league, int team)
        {
            var response = await _client._httpclient.GetAsync($"/v3/leagues?name={league}&team={team}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<League>(content);

            if (result.Response.Count != 0)
            {
                return result;
            }
            else { return null; }            
        }
        public async Task<League> GetLeague(string league, string country)
        {
            
            var response = await _client._httpclient.GetAsync($"/v3/leagues?name={league}&country={country}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<League>(content);

            if (result.Response.Count != 0)
            {
                return result;
            }
            else { return null; }
        }



    }
}
