namespace Craftfest.Models
{
    public class GameType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public string Rules { get; set; } = string.Empty;
        public int NumberOfTeams { get; set; }
        public int AvailableGames { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}