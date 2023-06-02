using System.ComponentModel.DataAnnotations.Schema;

namespace Craftfest.Models
{
    public class Game
    {
        public int Id { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public GameType Type { get; set; }
        public int WinnerId { get; set; }
        [NotMapped]
        public Team Winner { get; set; } = new Team();
    }
}
