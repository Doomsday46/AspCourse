using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspCourse.Models;
using Microsoft.AspNetCore.Authorization;
using AspCourse.Models.checkModel;

namespace AspCourse.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ApplicationContext _context;

        public TournamentsController(ApplicationContext context)
        {
            _context = context;
        }

        private void initDataView()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";
        }

        // GET: Tournaments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            initDataView();
            return View(await _context.Tournaments.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
        }

        private int GetIdUser()
        {
            try
            {
                initDataView();
                return _context.Users.First(a => a.Email.Equals(HttpContext.User.Identity.Name)).Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // GET: Tournaments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId.Equals(GetIdUser()));
            if (tournament == null)
            {
                return NotFound();
            }

            ViewBag.IsValid = new CheckModel(tournament, tournament.Players, tournament.Locations, tournament.Teams, tournament.PrizePlaces).IsValid();

            return View(tournament);
        }

        // GET: Tournaments/Create
        [Authorize]
        public IActionResult Create()
        {
            initDataView();
            return View();
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,DateTime")] Tournament tournament)
        {
            initDataView();
            tournament.UserId = GetIdUser();
            if (ModelState.IsValid)
            {
                _context.Add(tournament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateTime")] Tournament tournament)
        {
            initDataView();
            if (id != tournament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(tournament.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }


        [Authorize]
        public async Task<IActionResult> EditPlayer(int id)
        {
            try
            {
                initDataView();
                ViewBag.Tournament = _context.Tournaments.First(a => a.Id.Equals(id));
                return View(await _context.Players.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
            }
            catch (Exception)
            {
                return View();
            }
            
        }

        [Authorize]
        public async Task<IActionResult> EditLocation(int id)
        {
            try
            {
                initDataView();
                ViewBag.Tournament = _context.Tournaments.First(a => a.Id.Equals(id));
                return View(await _context.Locations.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> SubscribeTournament(int id, int idPlayer) {

            try
            {
                initDataView();
                Player player = _context.Players.First(a => a.Id.Equals(idPlayer));

                Tournament tournament = _context.Tournaments.First(a => a.Id.Equals(id));

                player.Tournament = tournament;
                player.TournamentId = tournament.Id;

                tournament.Players.Add(player);

                _context.Update(player);
                _context.Update(tournament);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            ViewBag.Tournament = _context.Tournaments.First(a => a.Id.Equals(id));
            return View("EditPlayer", await _context.Players.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UnSubscribeTournament(int id, int idPlayer)
        {

            try
            {
                initDataView();
                Player player = _context.Players.First(a => a.Id.Equals(idPlayer));

                Tournament tournament = _context.Tournaments.First(a => a.Id.Equals(id));

                tournament.Players.Remove(player);

                player.Tournament = null;
                player.TournamentId = null;

                _context.Update(player);
                _context.Update(tournament);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }

            ViewBag.Tournament = _context.Tournaments.First(a => a.Id.Equals(id));
            return View("EditPlayer", await _context.Players.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> SubscribeLocationToTournament(int id, int idLocation)
        {

            try
            {
                initDataView();
                Location location = _context.Locations.First(a => a.Id.Equals(idLocation));

                Tournament tournament = _context.Tournaments.First(a => a.Id.Equals(id));

                location.Tournament = tournament;
                location.TournamentId = tournament.Id;

                tournament.Locations.Add(location);

                _context.Update(location);
                _context.Update(tournament);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            ViewBag.Tournament = _context.Tournaments.First(a => a.Id.Equals(id));
            return View("EditLocation", await _context.Locations.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UnSubscribeLocationToTournament(int id, int idLocation)
        {

            try
            {
                initDataView();
                Location location = _context.Locations.First(a => a.Id.Equals(idLocation));

                Tournament tournament = _context.Tournaments.First(a => a.Id.Equals(id));

                tournament.Locations.Remove(location);

                location.Tournament = null;
                location.TournamentId = null;

                _context.Update(location);
                _context.Update(tournament);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }

            ViewBag.Tournament = _context.Tournaments.First(a => a.Id.Equals(id));
            return View("EditLocation", await _context.Locations.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
        }

        // GET: Tournaments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            initDataView();
            var tournament = await _context.Tournaments.FindAsync(id);
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowLocation(int id)
        {
            try
            {
                initDataView();
                return View(await _context.Locations.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser()) && m.TournamentId.Equals(id)).ToListAsync());
            }
            catch (Exception)
            {
                return View(new List<Location>());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowPlayer(int id)
        {
            try
            {
                initDataView();
                var teams = _context.Teams.Where(a => a.TournamentId.Equals(id) && a.UserId != null && a.UserId.Equals(GetIdUser()));
                SelectList selectLists = new SelectList(teams, "Id", "Name");
                ViewBag.Teams = selectLists;
                ViewData["tournamentId"] = id;
                return View(await _context.Players.Include(t => t.Team).Where(m => m.UserId != null && m.UserId.Equals(GetIdUser()) && m.TournamentId.Equals(id)).ToListAsync());
            }
            catch (Exception)
            {
                return View(new List<Player>());
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetTeam(int playerId, int teamId, int tournamentId)
        {           
            initDataView();
            var team = _context.Teams.FirstOrDefault(a => a.Id.Equals(teamId) && a.UserId.Equals(GetIdUser()));
            var player = _context.Players.FirstOrDefault(a => a.Id.Equals(playerId) && a.UserId.Equals(GetIdUser()));

            player.TeamId = teamId;
            player.Team = team;
            team.Players.Add(player);

            _context.Update(player);
            await _context.SaveChangesAsync();

            var teams = _context.Teams.Where(a => a.TournamentId.Equals(tournamentId) && a.UserId != null && a.UserId.Equals(GetIdUser()));
            SelectList selectLists = new SelectList(teams, "Id", "Name");
            ViewBag.Teams = selectLists;
            ViewData["tournamentId"] = tournamentId;

            return View("ShowPlayer", await _context.Players.Include(t => t.Team).Where(m => m.UserId != null && m.UserId.Equals(GetIdUser()) && m.TournamentId.Equals(tournamentId)).ToListAsync());  
        }
    }
}
