using Craftfest.Models;
using Microsoft.AspNetCore.SignalR;

namespace Craftfest.Hubs
{
    public class TournamentHub : Hub
    {
        public async Task UpdateWinner(int teamId, int gameId)
        {
            await Clients.All.SendAsync("WinnerUpdated", teamId, gameId);
        }
    }
}
