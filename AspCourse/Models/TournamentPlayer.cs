using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models
{
    public class TournamentPlayer
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public long TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
