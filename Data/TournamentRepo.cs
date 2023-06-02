using Craftfest.Interfaces;
using Craftfest.Models;
using Microsoft.EntityFrameworkCore;

namespace Craftfest.Data
{
    public class TournamentRepo : ITournamentRepo
    {
        private readonly ApplicationDbContext _context;

        public TournamentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTournament(Tournament tournament)
        {
            await _context.Tournaments.AddAsync(tournament);
        }

        public async Task DeleteTournament(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                throw new Exception("Tournament not found");
            }
            _context.Tournaments.Remove(tournament);
        }

        public async Task<Tournament> GetTournament(int id)
        {
            var tournament = _context.Tournaments
                .Include(x => x.Winner)
                .Include(x => x.Rounds).ThenInclude(x => x.Games).ThenInclude(x => x.Teams).AsParallel()
                .FirstOrDefault(x => x.Id == id) ?? new Tournament();
            return tournament;
        }

        public async Task<List<Tournament>> ListTournaments()
        {
            var tournaments = await _context.Tournaments
                .Include(x => x.Winner)
                .Include(x => x.Rounds).ThenInclude(x => x.Games).ThenInclude(x => x.Teams).ThenInclude(x => x.Players)
                .Include(x => x.Rounds).ThenInclude(x => x.Games).ThenInclude(x => x.Type)
                .ToListAsync();
            return tournaments;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateTournament(Tournament tournament)
        {
            _context.Tournaments.Update(tournament);
        }
    }
}
