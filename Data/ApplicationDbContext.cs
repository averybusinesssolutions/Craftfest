using Craftfest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Craftfest.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Game> Games { get; set; }
        //public DbSet<Player> Players { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        //public DbSet<Bracket> Brackets { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }

        private List<Round> GetRounds(List<Game> games, List<GameType> gameTypes)
        {
            List<Round> rounds = new List<Round>();
            while (games.Count > 0)
            {
                Round round = new Round();
                round.Games = NextRound(gameTypes, games);
                rounds.Add(round);

                foreach(var g in round.Games)
                {
                    games.Remove(g);
                }
            }
            return rounds;
        }

        private List<Game> NextRound(List<GameType> gameTypes, List<Game> games)
        {
            List<Game> output = new List<Game>();
            List<Team> currentlyPlaying = new List<Team>();
            foreach (var gt in gameTypes)
            {
                int availableGames = gt.AvailableGames;

                for (int i = 0; i < availableGames; i++)
                {
                    var game = games
                        .Where(x => x.Type.Name == gt.Name && !currentlyPlaying.Any(cp => cp?.Name == x.Teams[0]?.Name) && !currentlyPlaying.Any(cp => cp?.Name == x.Teams[1]?.Name))
                        .FirstOrDefault();
                    if (game != null)
                    {
                        currentlyPlaying.AddRange(game.Teams);
                        output.Add(game);
                        games.Remove(game);
                    }
                }
            }

            return output;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var gt = new List<GameType>() 
            {
                new GameType { 
                    Id = 1, 
                    Name = "Corn Hole", 
                    ImagePath = "images/Corn Hole.jpg", 
                    Description = "Toss bean bags onto specialized boards.", 
                    NumberOfTeams = 2, AvailableGames = 2, 
                    Rules = "<ul><li>Players on the same side will alternate throws</li><li>Individual points cancel each other out. Round score is determined by subtracting one score from the other</li><li>First team to 21 wins</li><li>To win, you must get exactly 21 to win. Overtime is a game to 11.</li><li>3 points = sand bag in the hole</li><li>1 point = sand bag on the board</li><li>A sandbag CANNOT hit the ground and bounce on the board. This is a zero point scenario</li><li>If you go over 21, the amount of points you go over will be subtracted from your score.</li></ul>"},
                new GameType { 
                    Id = 2, 
                    Name = "Kan Jam", 
                    ImagePath = "images/kanjam.jpg", 
                    Description = "Throw frisbees into trashcans.", 
                    NumberOfTeams = 2, AvailableGames = 2, 
                    Rules = "<ul><li>Play to 21 points</li><li>If a team goes over 21 points, those extra points are subtracted from the team’s current score</li><li>When a team scores 3 points on both throws, each team member throws again.</li><li>3 points = slapped in on top</li><li>2 points = direct KamJam hit without teammate interference</li><li>1 point = disc is directed into the outside of the KamJam by teammate</li><li>Slot = Automatic win</li><li>Overtime is a game to 11</li></ul>"
                },
                new GameType { 
                    Id = 3, 
                    Name = "Beer Pong", 
                    ImagePath = "images/beerpong.jpg", 
                    Description = "Toss ping pong balls into solo cups.", 
                    NumberOfTeams = 2, AvailableGames = 1,
                    Rules = "<ul><li>The rules are simplistic to mimic basic beer pong</li><li>Both eye for an eye OR rock, paper, scissors can be used to determine who goes first</li><li>If both players make a cup on their turn, the balls are returned to shoot again</li><li>There is no heating up, on fire, “blowing” / “finger” the ball from the cup or lightning</li><li>Teams get 1 (re-rack) request per game and can only be used at the start of a teams turn.</li><li>There is no end game gentlemens</li><li>If a team sinks all cups, the opposing players have redemption (chance to shoot again). If one of these players makes a cup, that player is afforded another shot until they miss.</li><li>Sudden death setup is a 3 cup triangle.</li></ul>"
                },
                new GameType { 
                    Id = 4, 
                    Name = "Potato Sack Race", 
                    ImagePath = "images/psr.jpg", 
                    Description = "Race to the finish with your legs in a potato sack.", 
                    NumberOfTeams = 2, AvailableGames = 1,
                    Rules = "<ul><li>A player from both teams starts the race by placing both feet into the sack.</li><li>The first players will hop down to the marked location and head back to the starting point.</li><li>Players MUST Tag the hand of their teammates before getting out of the sack. The tagged in teammates will hop the course and the first team back to the finish line wins.</li><li>IMPORTANT: an officiator must be present at every race to confirm winner and enforce rules.</li></ul>"
                },
                new GameType { 
                    Id = 5, 
                    Name = "Ladder Golf", 
                    ImagePath = "images/laddergolf.jpg", 
                    Description = "Throw bolas around poles on specialized ladder structure.", 
                    NumberOfTeams = 2, AvailableGames = 1,
                    Rules = "<ul><li>establish throw line at 5 paces from the ladder</li><li>Game is to 21 exactly. If a player goes over 21, all points are discarded from that round.</li><li>Bolla on the bottom ladder = 1 point</li><li>Bolla on the middle ladder = 2 points</li><li>Bolla on the top ladder = 3 points</li><li>If a Bolla from both teams end up on the same ladder, those points cancel out. IMPORTANT: points ONLY cancel out on the same ladder. If team A’s Bolla hits the top ladder and Team B’s hits the middle ladder those points DO NOT cancel out</li><li>Bolas can be knocked off the ladder. Bola’s knocked off the ladder do not count towards end of round scoring.</li><li>One (1) bonus point is awarded if a player lands all 3 Bolla’s on any ladder during their turn.</li></ul>"
                }
            };

            var teams = new List<Team>()
            {
                //good
                new Team { Id = 1, Name = "Texas"},
                //good
                new Team { Id = 2, Name = "Florida"},
                //good
                new Team { Id = 3, Name = "Tennesse"},
                //good
                new Team { Id = 4, Name = "Maryland"},
                //good
                new Team { Id = 5, Name = "Arizona"},
                //good
                new Team { Id = 6, Name = "Hawaii"},
                //good
                new Team { Id = 7, Name = "Indiana"},
                //new Team { Id = 8, Name = "Alaska"},
                //good
                new Team { Id = 9, Name = "California"},
                //good
                new Team { Id = 10, Name = "New Hampshire"},
                //good
                new Team { Id = 11, Name = "New Jersey"},
                //good
                new Team { Id= 12, Name = "Kentucky"},
                //good
                new Team { Id = 13, Name = "Louisiana"},
                //good
                new Team { Id = 14, Name = "North Carolina"}
            };

            var games = new List<Game>()
            {
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Texas").FirstOrDefault(), teams.Where(x => x.Name == "Maryland").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Kentucky").FirstOrDefault(), teams.Where(x => x.Name == "New Jersey").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "North Carolina").FirstOrDefault(), teams.Where(x => x.Name == "Arizona").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Texas").FirstOrDefault(), teams.Where(x => x.Name == "California").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Hawaii").FirstOrDefault(), teams.Where(x => x.Name == "Lousiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Tennessee").FirstOrDefault(), teams.Where(x => x.Name == "Indiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Jersey").FirstOrDefault(), teams.Where(x => x.Name == "Hawaii").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Tennessee").FirstOrDefault(), teams.Where(x => x.Name == "Louisiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Arizona").FirstOrDefault(), teams.Where(x => x.Name == "Indiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "North Carolina").FirstOrDefault(), teams.Where(x => x.Name == "Kentucky").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Florida").FirstOrDefault(), teams.Where(x => x.Name == "New Hampshire").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "California").FirstOrDefault(), teams.Where(x => x.Name == "Florida").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Hampshire").FirstOrDefault(), teams.Where(x => x.Name == "Maryland").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Kan Jam").FirstOrDefault(),
                },
                /////////////////////////////////////////////////////////////////////////////
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Kentucky").FirstOrDefault(), teams.Where(x => x.Name == "Indiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Lousiana").FirstOrDefault(), teams.Where(x => x.Name == "Arizona").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Hawaii").FirstOrDefault(), teams.Where(x => x.Name == "New Jersey").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "California").FirstOrDefault(), teams.Where(x => x.Name == "New Hampshire").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Tennessee").FirstOrDefault(), teams.Where(x => x.Name == "Texas").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Florida").FirstOrDefault(), teams.Where(x => x.Name == "Maryland").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Kentucky").FirstOrDefault(), teams.Where(x => x.Name == "North Carolina").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Hampshire").FirstOrDefault(), teams.Where(x => x.Name == "Tennessee").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "North Carolina").FirstOrDefault(), teams.Where(x => x.Name == "Florida").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Louisiana").FirstOrDefault(), teams.Where(x => x.Name == "Texas").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Jersey").FirstOrDefault(), teams.Where(x => x.Name == "Indiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Arizona").FirstOrDefault(), teams.Where(x => x.Name == "Maryland").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "California").FirstOrDefault(), teams.Where(x => x.Name == "Hawaii").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Corn Hole").FirstOrDefault(),
                },
                /////////////////////////////////////////////////////////////////////////////
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "North Carolina").FirstOrDefault(), teams.Where(x => x.Name == "Hawaii").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Florida").FirstOrDefault(), teams.Where(x => x.Name == "Kentucky").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "California").FirstOrDefault(), teams.Where(x => x.Name == "New Hampshire").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Maryland").FirstOrDefault(), teams.Where(x => x.Name == "Indiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Arizona").FirstOrDefault(), teams.Where(x => x.Name == "Tennessee").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Tennessee").FirstOrDefault(), teams.Where(x => x.Name == "New Jersey").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Texas").FirstOrDefault(), teams.Where(x => x.Name == "California").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Florida").FirstOrDefault(), teams.Where(x => x.Name == "New Hampshire").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Louisiana").FirstOrDefault(), teams.Where(x => x.Name == "Kentucky").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "North Carolina").FirstOrDefault(), teams.Where(x => x.Name == "Indiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Arizona").FirstOrDefault(), teams.Where(x => x.Name == "Hawaii").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Jersey").FirstOrDefault(), teams.Where(x => x.Name == "Maryland").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Texas").FirstOrDefault(), teams.Where(x => x.Name == "Louisiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Potato Sack Race").FirstOrDefault(),
                },
                ///////////////////////////////////////////////////////////////////////////
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Jersey").FirstOrDefault(), teams.Where(x => x.Name == "Indiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Hawaii").FirstOrDefault(), teams.Where(x => x.Name == "North Carolina").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Tennessee").FirstOrDefault(), teams.Where(x => x.Name == "Maryand").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Indiana").FirstOrDefault(), teams.Where(x => x.Name == "Hawaii").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Arizona").FirstOrDefault(), teams.Where(x => x.Name == "North Carolina").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Florida").FirstOrDefault(), teams.Where(x => x.Name == "New Jersey").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Louisiana").FirstOrDefault(), teams.Where(x => x.Name == "California").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Texas").FirstOrDefault(), teams.Where(x => x.Name == "Kentucky").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Hampshire").FirstOrDefault(), teams.Where(x => x.Name == "Maryland").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Tennessee").FirstOrDefault(), teams.Where(x => x.Name == "Texas").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Arizona").FirstOrDefault(), teams.Where(x => x.Name == "Louisiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Hampshire").FirstOrDefault(), teams.Where(x => x.Name == "California").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Kentucky").FirstOrDefault(), teams.Where(x => x.Name == "Florida").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Ladder Golf").FirstOrDefault(),
                },
                /////////////////////////////////////////////////////////////////////////////
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Tennessee").FirstOrDefault(), teams.Where(x => x.Name == "Texas").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Arizona").FirstOrDefault(), teams.Where(x => x.Name == "Maryland").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Indiana").FirstOrDefault(), teams.Where(x => x.Name == "Louisiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Hawaii").FirstOrDefault(), teams.Where(x => x.Name == "Florida").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Arizona").FirstOrDefault(), teams.Where(x => x.Name == "California").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Jersey").FirstOrDefault(), teams.Where(x => x.Name == "New Hampshire").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Tennessee").FirstOrDefault(), teams.Where(x => x.Name == "California").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Texas").FirstOrDefault(), teams.Where(x => x.Name == "North Carolina").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "North Carolina").FirstOrDefault(), teams.Where(x => x.Name == "Kentucky").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Kentucky").FirstOrDefault(), teams.Where(x => x.Name == "Maryland").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Florida").FirstOrDefault(), teams.Where(x => x.Name == "New Jersey").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "Hawaii").FirstOrDefault(), teams.Where(x => x.Name == "Indiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                },
                new Game
                { Teams = new List<Team>{
                    teams.Where(x => x.Name == "New Hampshire").FirstOrDefault(), teams.Where(x => x.Name == "Lousiana").FirstOrDefault() },
                    Type = gt.Where(x => x.Name == "Beer Pong").FirstOrDefault(),
                }
            };

            var tournament = new Tournament {
                Id = 1,
                TournamentDate = DateTime.Now.AddDays(1).ToUniversalTime(),
                Name = "Craftfest 2022",
                Bracket = new Bracket()
                {
                    Games = games,
                    GameTypeReplays = 2,
                    GameTypes = gt
                },
                Rounds = GetRounds(games, gt)
            };



            modelBuilder.Entity<GameType>().HasData(gt);
            modelBuilder.Entity<Team>().HasData(teams);
            //modelBuilder.Entity<Tournament>().HasData(tournament);
            //modelBuilder.Entity<Round>().HasData(tournament.Rounds);

            base.OnModelCreating(modelBuilder);
        }
    }
}