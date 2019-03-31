using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models.database.entity
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int? UserId { get; set; }
        public User User { get; set; }

        public int? TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public ICollection<Player> Players { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }
    }
}
