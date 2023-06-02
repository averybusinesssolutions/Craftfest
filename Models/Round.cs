namespace Craftfest.Models
{
    public class Round
    {
        public int Id { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
