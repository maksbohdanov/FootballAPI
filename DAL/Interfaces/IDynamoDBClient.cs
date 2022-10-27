using Amazon.DynamoDBv2.Model;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDynamoDBClient
    {
        Task<GetItemResponse> GetTeamByName(string name);
        void PostTeam(TeamInfoDb data);

        Task<ScanResponse> GetAllAsync(string chat);
        Task<bool> PostFixture(FixtureDb data);

        Task<bool> Delete(string id);
    }
}
