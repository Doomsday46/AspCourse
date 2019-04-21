using AspCourse.Models;
using AspCourse.Models.checkModel;
using AspCourse.Models.database.entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspcourseTests
{
    public class CheckModelTests
    {
        private List<Player> players;
        private List<Team> teams;
        private List<Location> locations;
        private List<PrizePlace> prizePlace;
        private Tournament tournament;

        [SetUp]
        public void Setup()
        {
            players = new List<Player>();
            var n = 4;
            for (int i = 0; i < n; i++)
            {
                players.Add(new Player() {
                    FirstName = "MockName" + i,
                    SecondName = "MockSecondName" + i,
                    BirthDay = new DateTime(1997, 05, i + 2)
                });
            }

            teams = new List<Team>() {
                new Team() {
                    Name = "Team1",
                    Players = players.GetRange(0, 2)
                   
                },
                new Team() {
                    Name = "Team2",
                    Players = players.GetRange(2, 2)
                }
            };         

            locations = new List<Location>() {
                new Location(){
                    Name = "Table1",
                    Description = "Description"
                }
            };

            prizePlace = new List<PrizePlace>() {
                new PrizePlace(){
                    Number = 1
                }
            };


            tournament = new Tournament() {
                Name = "Tournament",
                DateTime = DateTime.Now.AddDays(-4),
                Players = players,
                Locations = locations,
                Teams = teams,
                PrizePlaces = prizePlace
            };

        }  
           
        [Test]
        public void TestInitClass()
        {
            new CheckModel(tournament, players, locations, teams, prizePlace);
        }

        [Test]
        public void TestIsValid()
        {
            var checkModelTournament = new CheckModel(tournament, players, locations, teams, prizePlace);

            Assert.IsTrue(checkModelTournament.IsValid());
        }

        [Test]
        public void TestIsValidLocations()
        {
            var checkModelTournament = new CheckModel(tournament, players, locations, teams, prizePlace);

            Assert.IsTrue(checkModelTournament.IsValidLocations());
        }

        [Test]
        public void TestIsValidPlayers()
        {
            var checkModelTournament = new CheckModel(tournament, players, locations, teams, prizePlace);

            Assert.IsTrue(checkModelTournament.IsValidPlayers());
        }

        [Test]
        public void TestIsValidPrizePlace()
        {
            var checkModelTournament = new CheckModel(tournament, players, locations, teams, prizePlace);

            Assert.IsTrue(checkModelTournament.IsValidPrizePlace());
        }

        [Test]
        public void TestIsValidTeams()
        {
            var checkModelTournament = new CheckModel(tournament, players, locations, teams, prizePlace);

            Assert.IsTrue(checkModelTournament.IsValidTeams());
        }

        [Test]
        public void TestEquals()
        {
            var checkModelTournament = new CheckModel(tournament, players, locations, teams, prizePlace);

            Assert.IsTrue(checkModelTournament.Equals(tournament));
        }
    }
}
