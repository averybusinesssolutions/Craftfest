using System.ComponentModel.DataAnnotations.Schema;

namespace Craftfest.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public double Score { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();

        public override string ToString() => Name;

        // Note: this is important so the MudSelect can compare pizzas
        public override bool Equals(object o)
        {
            var other = o as Team;
            return other?.Name == Name;
        }

        // Note: this is important too!
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        public string GetShortName()
        {
            string name = string.Empty;
            if (Name.Contains(' '))
            {
                var split = Name.Split(' ');
                name = split[0].ToUpper().Substring(0,1) + split[1].ToUpper().Substring(0,1);
            }
            else
            {
                name = Name.ToUpper().Substring(0, 2);
            }
            return name;
        }
    }
}
