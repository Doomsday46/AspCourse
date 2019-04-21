using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models.checkModel
{
    public interface ICheckModelTournament
    {
        bool IsValid();
        bool Equals(Tournament tournament);
        bool ValidTimeSetting();
        bool IsValidPlayers();
        bool IsValidLocations();
        bool IsValidPrizePlace();
        bool IsValidTeams();
    }
}
