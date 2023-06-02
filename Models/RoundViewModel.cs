namespace Craftfest.Models
{
    public class RoundViewModel
    {
        public List<Round> Rounds { get; set; }
        public List<Game> CurrentRound { get; set; }
        public List<GameType> GameTypes { get; set; }
    }
}
