using Craftfest.Models;

namespace Craftfest.Data {
    public class BracketRepo : IBracketRepo
    {
        private readonly ApplicationDbContext _context;

        public BracketRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GameType> GetGameTypes()
        {
            return _context.GameTypes.ToList();
        }

        public IEnumerable<Team> GetTeams()
        {
            return _context.Teams.ToList();
        }
    }
}