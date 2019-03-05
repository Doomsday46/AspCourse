using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models
{
    public class Player
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }

        public ICollection<TournamentPlayer> TournamentPlayers { get; set; }

        public Player()
        {
            TournamentPlayers = new List<TournamentPlayer>();
        }

        public override bool Equals(object obj)
        {
            var player = obj as Player;
            return player != null &&
                   Id == player.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
