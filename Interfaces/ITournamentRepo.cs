using Craftfest.Models;

namespace Craftfest.Interfaces
{
    public interface ITournamentRepo
    {
        Task<Tournament> GetTournament(int id);
        Task<List<Tournament>> ListTournaments();
        void UpdateTournament(Tournament tournament);
        Task DeleteTournament(int id);
        Task<bool> SaveChanges();
        Task AddTournament(Tournament tournament);
    }
}
