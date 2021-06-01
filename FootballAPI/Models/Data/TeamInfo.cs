using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballAPI.Models.Data
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class VenueTeamInfo
    {
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class ResponseTeamInfo
    {
        public Team Team { get; set; }
        public VenueTeamInfo Venue { get; set; }
    }

    public class TeamInfo
    {
        public List<ResponseTeamInfo> Response { get; set; }
    }


}
