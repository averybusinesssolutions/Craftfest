using Craftfest.Data;

namespace Craftfest.Models
{
    public class Bracket
    {
        private readonly IBracketRepo _bracketRepo;

        public List<Game> Games { get; set; }
        public List<Team> Teams { get; set; }

        public int GameTypeReplays { get; set; }
        public List<GameType> GameTypes { get; set; }

        public Bracket(IBracketRepo bracketRepo)
        {
            _bracketRepo = bracketRepo;
            Games = new List<Game>();
            Teams = _bracketRepo.GetTeams().OrderBy(x => x.Id).ToList();
            GameTypeReplays = 2;
            GameTypes = _bracketRepo.GetGameTypes().ToList();
        }

        public Bracket()
        {
            Games = new List<Game>();
        }

        public void Initialize()
        {
            var teams = Teams;
            List<Game> games = new List<Game>();
            for (int i = 0; i < GameTypeReplays; i++)
            {
                games = new List<Game>();
                foreach (var gt in GameTypes)
                {
                    var teamsPlayed = games.Where(x => x.Type.Name == gt.Name).SelectMany(x => x.Teams).ToList();
                    var teamsAvailalbe = teams.Where(x => !teamsPlayed.Where(y => y.Name == x.Name).Any()).ToList();
                    while (teamsAvailalbe.Any())
                    {
                        teamsPlayed = games.Where(x => x.Type.Name == gt.Name).SelectMany(x => x.Teams).ToList();
                        teamsAvailalbe = teamsAvailalbe = teams.Where(x => !teamsPlayed.Where(y => y.Name == x.Name).Any()).ToList();
                        if (!teamsAvailalbe.Any())
                        {
                            break;
                        }
                        if (teamsAvailalbe.Count < gt.NumberOfTeams)
                        {
                            Random rnd = new Random();
                            teamsAvailalbe.Add(teamsPlayed.OrderBy(x => x?.Games?.Count).ToList()[0]);
                        }
                        (List<Game>, List<Team>) round = Randomize(teamsAvailalbe, gt);
                        foreach (var game in round.Item1)
                        {
                            Games.Add(game);
                            games.Add(game);
                            game.Teams[0].Games.Add(game);
                            game.Teams[1].Games.Add(game);
                            Console.WriteLine($"{game.Type.Name} {game.Teams[0].Name} vs {game.Teams[1].Name}");
                        }
                    }
                }
            }
        }

        public (List<Game>, List<Team>) Randomize(List<Team> teams, GameType gameType)
        {
            List<Game> round = new List<Game>();
            List<Team> teamsPlayed = new List<Team>();

            //for (int i = 0; i < GameTypeReplays; i++)
            //{
            var game = new Game() { Type = gameType };
            for (int j = 0; j < gameType.NumberOfTeams; j++)
            {
                Random rnd = new Random();
                Team team = teams[rnd.Next(teams.Count())];
                while (teamsPlayed.Contains(team))
                {
                    if (teamsPlayed.Count() == teams.Count && teamsPlayed.Count() > 0)
                    {
                        return (round, teamsPlayed);
                    }
                    team = teams[rnd.Next() % teams.Count()];
                }

                game.Teams.Add(team);
                teamsPlayed.Add(team);
            }
            round.Add(game);
            //}

            return (round, teamsPlayed);
        }
    }
}
