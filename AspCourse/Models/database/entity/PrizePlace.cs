using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models.database.entity
{
    public class PrizePlace
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int? TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
