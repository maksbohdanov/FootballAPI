using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DAL.Interfaces;
using DAL.Entities;

namespace DAL.Clients
{
    public class DynamoDBClient : IDynamoDBClient, IDisposable
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public DynamoDBClient(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDb = dynamoDB;
        }
        

        public async Task<ScanResponse> GetAllAsync(string chat)
        {           
            var request = new ScanRequest
            {
                TableName = "Fixtures",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":val", new AttributeValue { S = chat } }
                },
                FilterExpression = "IdChat = :val",
            };

            var response = await _dynamoDb.ScanAsync(request);

            if (response.Items == null || response.Items.Count == 0)
                return null;

            return response;

            //foreach (Dictionary<string, AttributeValue> item in response.Items)
            //{
            //    result.Add(item.ToClass<FixtureDBResponse>());

            //}
            //return result;
        }

        public async Task<bool> PostFixture(FixtureDb data)
        {
            Random rnd = new Random();
            var request = new PutItemRequest
            {
                TableName = "Fixtures",
                Item = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue { S = rnd.Next().ToString() } },
                    {"IdChat", new AttributeValue { S = data.IdChat ?? "no data" } },
                    {"IdFixture", new AttributeValue { N = data.IdFixture.ToString() } },
                    {"Date", new AttributeValue { S = data.Date ?? "no data"} },
                    {"Stadium", new AttributeValue { S = data.Stadium ?? "no data" } },
                    {"City", new AttributeValue { S = data.City ?? "no data" } },
                    {"Status", new AttributeValue { S = data.Status  } },
                    {"IdLeague", new AttributeValue { N = data.IdLeague.ToString()  } },
                    {"League", new AttributeValue { S = data.League  } },
                    {"Country", new AttributeValue { S = data.Country ?? "no data"} },
                    {"Season", new AttributeValue { N = data.Season.ToString()  } },

                    {"Home", new AttributeValue { S = data.Home ?? "no data" } },
                    {"Draw", new AttributeValue { S = data.Draw ?? "no data" } },
                    {"Away", new AttributeValue { S = data.Away ?? "no data"} },


                    {"IdHome", new AttributeValue { N = data.IdHome.ToString()  } },
                    {"NameHome", new AttributeValue { S = data.NameHome  } },
                    {"GoalHome", new AttributeValue { S = data.GoalHome !=  null ? data.GoalHome.ToString() : "no data" } },

                     {"IdAway", new AttributeValue { N = data.IdAway.ToString()  } },
                    {"NameAway", new AttributeValue { S = data.NameAway  } },
                    {"GoalAway", new AttributeValue { S = data.GoalAway != null ?  data.GoalAway.ToString() : "no data" } },
                }
            };

            try
            {
                var response = await _dynamoDb.PutItemAsync(request);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is your error \n" + e);
                return false;
            }
        }


        public async Task<GetItemResponse> GetTeamByName(string name)
        {
            var item = new GetItemRequest
            {
                TableName = "Teams",
                Key = new Dictionary<string, AttributeValue>
                    {
                        {"Name", new AttributeValue{S = name.Trim().ToLower()} }
                    },

            };

            var response = await _dynamoDb.GetItemAsync(item);

            if (response.Item == null || !response.IsItemSet)
                return null;
            return response;
            //var result = response.Item.ToClass<TeamInfoDBResponse>();
        }

        public async void PostTeam(TeamInfoDb data)
        {
            var request = new PutItemRequest
            {
                TableName = "Teams",
                Item = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue { N = data.Id.ToString() } },
                    {"Name", new AttributeValue { S = data.Name.Trim().ToLower() } },
                    {"Country", new AttributeValue { S = data.Country  } },
                }
            };

            try
            {
                await _dynamoDb.PutItemAsync(request);
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is your error \n" + e);
            }
        }
        

        public async Task<bool> Delete(string id)
        {            var request = new DeleteItemRequest
            {
                TableName = "Fixtures",
                Key = new Dictionary<string, AttributeValue>() { { "Id", new AttributeValue { S = id } } },

            };

            try
            {
                var response = await _dynamoDb.DeleteItemAsync(request);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is your error \n" + e);
                return false;
            }
        }

        public void Dispose()
        {
            _dynamoDb.Dispose();
        }
    }
}
