using Craftfest.Models;
using Microsoft.EntityFrameworkCore;

namespace Craftfest.Data
{
    public class GameRepo : IGameRepo
    {
        private readonly ApplicationDbContext _context;

        public GameRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Game> GetGameById(int id)
        {
            return _context.Games.Include(x => x.Teams).ThenInclude(x => x.Players).AsParallel().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Game>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateGame(Game game)
        {
            _context.Games.Update(game);
        }
    }
}
