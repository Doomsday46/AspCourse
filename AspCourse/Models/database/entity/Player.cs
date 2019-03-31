using AspCourse.Models.database.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }

        public int? TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public Player()
        {
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
