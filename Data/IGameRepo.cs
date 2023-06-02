using Craftfest.Models;

namespace Craftfest.Data
{
    public interface IGameRepo
    {
        Task<List<Game>> GetGames();
        Task<Game> GetGameById(int id);
        void UpdateGame(Game game);
        Task<bool> SaveChanges();
    }
}