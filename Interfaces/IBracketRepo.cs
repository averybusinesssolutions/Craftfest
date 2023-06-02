using Craftfest.Models;

public interface IBracketRepo
{
    IEnumerable<GameType> GetGameTypes();
    IEnumerable<Team> GetTeams();
}