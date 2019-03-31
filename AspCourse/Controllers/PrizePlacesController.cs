using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspCourse.Models;
using AspCourse.Models.database.entity;
using Microsoft.AspNetCore.Authorization;

namespace AspCourse.Controllers
{
    public class PrizePlacesController : Controller
    {
        private readonly ApplicationContext _context;

        public PrizePlacesController(ApplicationContext context)
        {
            _context = context;
        }

        private void initDataView()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";
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

        // GET: PrizePlaces
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var tournament = _context.Tournaments.Include(t => t.Players).Include(t => t.Locations).Include(t => t.Teams).FirstOrDefault(a => a.Id.Equals(id) && a.UserId.Equals(GetIdUser()));

            var countTeams = tournament.Teams.Count();

            if (countTeams < 5)
            {
                var prizePlace = new PrizePlace() { Number = 1, Tournament = tournament, TournamentId = tournament.Id, UserId = GetIdUser() };

                if (!_context.PrizePlaces.Any(a => a.Number.Equals(1) && a.TournamentId.Equals(tournament.Id))) {
                    _context.Add(prizePlace);
                    _context.SaveChanges();
                }
            }
            else {
                for (int i = 0; i < 2; i++)
                {
                   var prizePlace = new PrizePlace() { Number = i + 1, Tournament = tournament, TournamentId = tournament.Id, UserId = GetIdUser() };                  
                    if (!_context.PrizePlaces.Any(a => a.Number.Equals(i + 1) && a.TournamentId.Equals(tournament.Id))) {
                        _context.Add(prizePlace);
                        _context.SaveChanges();
                    }
                }
            }

            var applicationContext = _context.PrizePlaces.Include(t => t.Team).Where(a => a.UserId.Equals(GetIdUser()) && a.TournamentId.Equals(id)) ;

            return View(await applicationContext.ToListAsync());
        }

        // GET: PrizePlaces/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prizePlace = await _context.PrizePlaces
                .Include(p => p.Team)
                .Include(p => p.Tournament)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prizePlace == null)
            {
                return NotFound();
            }

            return View(prizePlace);
        }

        // GET: PrizePlaces/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prizePlace = await _context.PrizePlaces.FindAsync(id);
            if (prizePlace == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", prizePlace.TeamId);
            ViewBag.TournamentId = prizePlace.TournamentId;
            return View(prizePlace);
        }

        // POST: PrizePlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,TeamId")] PrizePlace prizePlace)
        {
            if (id != prizePlace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatePrizePlace = _context.PrizePlaces.FirstOrDefault(a => a.Id.Equals(id));
                    updatePrizePlace.TeamId = prizePlace.TeamId;
                    _context.Update(updatePrizePlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrizePlaceExists(prizePlace.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var _prizePlace = _context.PrizePlaces.Include(t => t.Tournament).FirstOrDefault(a => a.Id.Equals(prizePlace.Id));
                return RedirectToAction(nameof(Index),new { id = _prizePlace.TournamentId });
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", prizePlace.TeamId);
            ViewBag.TournamentId = prizePlace.TournamentId;
            return View(prizePlace);
        }

        // GET: PrizePlaces/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prizePlace = await _context.PrizePlaces
                .Include(p => p.Team)
                .Include(p => p.Tournament)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prizePlace == null)
            {
                return NotFound();
            }

            return View(prizePlace);
        }

        // POST: PrizePlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prizePlace = await _context.PrizePlaces.FindAsync(id);
            _context.PrizePlaces.Remove(prizePlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrizePlaceExists(int id)
        {
            return _context.PrizePlaces.Any(e => e.Id == id);
        }
    }
}
