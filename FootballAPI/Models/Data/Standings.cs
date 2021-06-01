using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballAPI.Models.Data
{
    public class TeamStandings
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GoalsStandings
    {
        public int For { get; set; }
        public int Against { get; set; }
    }

    public class AllStandings
    {
        public int Played { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public GoalsStandings Goals { get; set; }
    }

    public class Standing
    {
        public int Rank { get; set; }
        public TeamStandings Team { get; set; }
        public int Points { get; set; }
        public int GoalsDiff { get; set; }
        public AllStandings All { get; set; }
    }


    public class LeagueStandings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Season { get; set; }
        public List<List<Standing>> Standings { get; set; }
    }

    public class ResponseStandings
    {
        public LeagueStandings League { get; set; }
    }


    public class Standings
    {
        public List<ResponseStandings> Response { get; set; }
    }


}

