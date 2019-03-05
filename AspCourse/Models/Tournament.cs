using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models
{
    public class Tournament
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<TournamentPlayer> TournamentPlayers { get; set; }
        public ICollection<TournamentLocation> TournamentLocations { get; set; }

        public Tournament()
        {
            TournamentPlayers = new List<TournamentPlayer>();
            TournamentLocations = new List<TournamentLocation>();
        }

        public override bool Equals(object obj)
        {
            var tournament = obj as Tournament;
            return tournament != null &&
                   Id == tournament.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
