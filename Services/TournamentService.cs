using Craftfest.Data;
using Craftfest.Interfaces;
using Craftfest.Models;

namespace Craftfest.Services
{
    public class TournamentService
    {
        private readonly ITournamentRepo _repo;
        private readonly IGameRepo _gameRepo;
        private readonly ITeamRepo _teamRepo;

        private List<Tournament> _tournamentList;

        public TournamentService(ITournamentRepo repo, IGameRepo gameRepo, ITeamRepo teamRepo)
        {
            _repo = repo;
            _gameRepo = gameRepo;
            _teamRepo = teamRepo;
            _tournamentList = new List<Tournament>();
        }

        public Team GetTeam(int teamId, int tournamentId)
        {
            var teams = _tournamentList.Where(x => x.Id == tournamentId).SelectMany(x => x.Rounds).SelectMany(x => x.Games).SelectMany(x => x.Teams);
            return teams.FirstOrDefault(x => x.Id == teamId);
        }

        public void DeclareWinner(int teamId, int gameId)
        {
            Game game = GetGame(gameId, 1);
            game.Winner = GetTeam(teamId, 1);
            game.WinnerId = game.Winner.Id;
            game.Winner.Score += 2;
        }

        public Tournament GetTournamentById(int id)
        {
            return _tournamentList.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Tournament> GetTournaments()
        {
            return _tournamentList;
        }

        public void Update(Tournament tournament)
        {
            _repo.UpdateTournament(tournament);
        }

        public void AddAsync(Tournament tournament)
        {
            if(!_tournamentList.Any(x => x.Id == tournament.Id))
                _tournamentList.Add(tournament);
        }

        public Game GetGame(int gameId, int tournamentId)
        {
            var games = _tournamentList.Where(x => x.Id == tournamentId).SelectMany(x => x.Rounds).SelectMany(x => x.Games);
            return games.FirstOrDefault(x => x.Id == gameId);
        }

        public async Task<List<Team>> GetTeams(int tournamentId)
        {
            var tournament = GetTournamentById(tournamentId);
            IEnumerable<Team> teams = tournament.Rounds.SelectMany(x => x.Games).SelectMany(x => x.Teams).Distinct();
            return teams.ToList();
        }
    }
}
