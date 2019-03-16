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

        public ICollection<Player> Players{ get; set; }
        public ICollection<Location> Locations { get; set; }

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
