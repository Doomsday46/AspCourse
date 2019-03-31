using AspCourse.Models.database.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<PrizePlace> PrizePlaces { get; set; }

        public Tournament()
        {
            Players = new List<Player>();
            Locations = new List<Location>();
            Teams = new List<Team>();
            PrizePlaces = new List<PrizePlace>();
        }

        public override bool Equals(object obj)
        {
            var tournament = obj as Tournament;
            return tournament != null &&
                   Id == tournament.Id &&
                   Name == tournament.Name &&
                   DateTime == tournament.DateTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, DateTime);
        }
    }
}
