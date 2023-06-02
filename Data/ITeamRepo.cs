using Craftfest.Models;

namespace Craftfest.Data
{
    public interface ITeamRepo
    {
        Task<Team> GetTeamById(int id);
        Task<List<Team>> GetAllTeams();
        void Update(Team team);
    }
}