using AspCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.ViewModels
{
    public class TournamentPlayerModel
    {
        public ICollection<Player> Players { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }
    }
}
