namespace BLL.Models
{
    public class LeagueInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ResponseLeague
    {
        public LeagueInfo League { get; set; }
        public Country Country { get; set; }
    }

    public class League
    {
        public List<ResponseLeague> Response { get; set; }
    }
}
