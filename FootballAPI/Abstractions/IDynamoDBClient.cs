using FootballAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballAPI.Abstractions
{
    public interface IDynamoDBClient
    {
        Task<bool> PostFixture(FixtureDBResponse data);


        Task<TeamInfoDBResponse> GetTeamByName(string name);
        void PostTeam(TeamInfoDBResponse data);

        Task<List<FixtureDBResponse>> GetAll(string chat);

        Task<bool> Delete(string id);
        
    }
}
