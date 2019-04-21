using AspCourse.Models.database.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models.checkModel
{
    public class CheckModel : ICheckModelTournament
    {
        private readonly Tournament tournament;
        private readonly ICollection<Player> players;
        private readonly ICollection<Location> locations;
        private readonly ICollection<Team> teams;
        private readonly ICollection<PrizePlace> prizePlaces;

        public CheckModel(Tournament tournament, ICollection<Player> players, ICollection<Location> locations, ICollection<Team> teams, ICollection<PrizePlace> prizePlaces)
        {
            this.tournament = tournament ?? throw new ArgumentNullException(nameof(tournament));
            this.players = players ?? throw new ArgumentNullException(nameof(players));
            this.locations = locations ?? throw new ArgumentNullException(nameof(locations));
            this.teams = teams ?? throw new ArgumentNullException(nameof(teams));
            this.prizePlaces = prizePlaces ?? throw new ArgumentNullException(nameof(prizePlaces));
        }

        public bool Equals(Tournament tournament)
        {
            return this.tournament.Name.Equals(tournament.Name) && this.tournament.DateTime.Equals(tournament.DateTime);
        }

        public bool IsValid()
        {
            return IsValidLocations() && IsValidPlayers() && IsValidPrizePlace() && IsValidTeams() && ValidTimeSetting();
        }

        public bool IsValidLocations()
        {
            return !locations.GroupBy(x => x.Name).Any(g => g.Count() > 2);
        }

        public bool IsValidPlayers()
        {
            return players.Any(a => a.BirthDay.Year < DateTime.Now.Year - 12);
        }

        public bool IsValidPrizePlace()
        {
            return prizePlaces.Count <= players.Count && !prizePlaces.GroupBy(x => x.Number).Any(g => g.Count() > 2);
        }

        public bool IsValidTeams()
        {
            return !teams.Any(a => a.Players.Count == 0) && !teams.GroupBy(x => x.Name).Any(g => g.Count() > 2);
        }

        public bool ValidTimeSetting()
        {
            return tournament.DateTime.CompareTo(DateTime.Now) < 0;
        }

        
    }
}
