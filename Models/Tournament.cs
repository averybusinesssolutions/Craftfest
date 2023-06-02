using System.ComponentModel.DataAnnotations.Schema;

namespace Craftfest.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public List<Round> Rounds { get; set; } = new List<Round>();
        public DateTime TournamentDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public Team? Winner { get; set; }
        [NotMapped]
        public Bracket Bracket { get; set; }
    }
}
