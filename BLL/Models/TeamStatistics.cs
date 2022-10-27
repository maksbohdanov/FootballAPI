namespace BLL.Models
{
    public class LeagueTeamStat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
    }

    public class TeamTeamStat
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Played
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class Wins
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class Draws
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class Loses
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class Fixtures
    {
        public Played Played { get; set; }
        public Wins Wins { get; set; }
        public Draws Draws { get; set; }
        public Loses Loses { get; set; }
    }

    public class TotalScoredResult
    {
        public int Total { get; set; }
    }

    public class Average
    {
        public string Total { get; set; }
    }


    public class For
    {
        public TotalScoredResult Total { get; set; }
        public Average Average { get; set; }
    }

    public class Against
    {
        public TotalScoredResult Total { get; set; }
        public Average Average { get; set; }
    }

    public class GoalsTeamStat
    {
        public For For { get; set; }
        public Against Against { get; set; }
    }

    public class CleanSheet
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class ScoredPenalty
    {
        public int Total { get; set; }
        public string Percentage { get; set; }
    }

    public class MissedPenalty
    {
        public int TotalResult { get; set; }
        public string Percentage { get; set; }
    }

    public class Penalty
    {
        public ScoredPenalty Scored { get; set; }
        public MissedPenalty Missed { get; set; }
        public int Total { get; set; }
    }

    public class ResponseTeamStat
    {
        public LeagueTeamStat League { get; set; }
        public TeamTeamStat Team { get; set; }
        public Fixtures Fixtures { get; set; }
        public GoalsTeamStat Goals { get; set; }
        public CleanSheet CleanSheet { get; set; }
        public Penalty Penalty { get; set; }
    }

    public class TeamStatictics
    {
        public ResponseTeamStat Response { get; set; }
    }
}
