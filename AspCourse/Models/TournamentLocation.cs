using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models
{
    public class TournamentLocation
    {
        public long Id { get; set; }
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public long TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
