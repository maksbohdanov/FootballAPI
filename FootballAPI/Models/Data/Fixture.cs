using FootballAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballAPI.Models.Data
{
    public class VenueFixture
    {
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class StatusFixture
    {
        public string Long { get; set; }
        public string Short { get; set; }
        public int? Elapsed { get; set; }
    }

    public class FixtureFixture
    {
        public int? Id { get; set; }
        public string Referee { get; set; }
        public DateTime? Date { get; set; }
        public VenueFixture Venue { get; set; }
        public StatusFixture Status { get; set; }
    }

    public class LeagueFixture
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int? Season { get; set; }
    }

    public class HomeFixture
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? Winner { get; set; }
    }

    public class AwayFixture
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? Winner { get; set; }
    }
    public class Home
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Away
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class TeamsFixture
    {
        public Home Home { get; set; }
        public Away Away { get; set; }
    }

    public class GoalsFixture
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }    

    public class TimeFixture
    {
        public int? Elapsed { get; set; }
    }

    public class TeamFixture
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class ResponseFixure
    {
        public FixtureFixture Fixture { get; set; }
        public LeagueFixture League { get; set; }
        public TeamsFixture Teams { get; set; }
        public GoalsFixture Goals { get; set; }
        public List<Odds> Odds { get; set; }
        
    }

    public class Fixture
    {
        public List<ResponseFixure> Response { get; set; }
    }

}
