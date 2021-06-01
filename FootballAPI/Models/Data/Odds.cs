using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballAPI.Models.Data
{

    public class ValueOdds
    {
        public string Value { get; set; }
        public string Odd { get; set; }
    }

    public class Bet
    {
        public List<ValueOdds> Values { get; set; }
    }

    public class Bookmaker
    {
        public List<Bet> Bets { get; set; }
    }

    public class ResponseOdds
    {
        public List<Bookmaker> Bookmakers { get; set; }
    }

    public class Odds
    {
        public List<ResponseOdds> Response { get; set; }
    }




}
