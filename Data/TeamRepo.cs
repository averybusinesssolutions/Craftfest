using Craftfest.Models;
using Microsoft.EntityFrameworkCore;

namespace Craftfest.Data
{
    public class TeamRepo : ITeamRepo
    {
        private readonly ApplicationDbContext _context;

        public TeamRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetAllTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await _context.Teams.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Team team)
        {
            _context.Teams.Update(team);
        }
    }
}
