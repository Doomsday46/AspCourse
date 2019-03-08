using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspCourse.Models;
using AspCourse.ViewModels;

namespace AspCourse.Controllers
{
    public class TournamentPlayersController : Controller
    {
        private readonly ApplicationContext _context;

        public TournamentPlayersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: TournamentPlayers
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.TournamentPlayer.Include(t => t.Player).Include(t => t.Tournament);
            return View(await applicationContext.ToListAsync());
        }

        // GET: TournamentPlayers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentPlayer = await _context.TournamentPlayer
                .Include(t => t.Player)
                .Include(t => t.Tournament)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (tournamentPlayer == null)
            {
                return NotFound();
            }

            return View(tournamentPlayer);
        }

        // GET: TournamentPlayers/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name");
            return View();
        }

        // POST: TournamentPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,TournamentId")] TournamentPlayer tournamentPlayer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tournamentPlayer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", tournamentPlayer.PlayerId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name", tournamentPlayer.TournamentId);
            return View(tournamentPlayer);
        }

        // GET: TournamentPlayers/Edit/5
        public async Task<IActionResult> Edit(TournamentPlayer tournamentPlayerModel)
        {
            if (tournamentPlayerModel == null)
            {
                return NotFound();
            }

            if (!_context.TournamentPlayer.Any(a => a.Id.Equals(tournamentPlayerModel.Id)))
            {
                return NotFound();
            }
            ViewData["TournamentPlayerId"] = tournamentPlayerModel.Id;
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", tournamentPlayerModel.PlayerId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name", tournamentPlayerModel.TournamentId);
            return View(tournamentPlayerModel);
        }

        // POST: TournamentPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id")] long id, [Bind("PlayerId,TournamentId")] TournamentPlayer tournamentPlayer)
        {
            if (tournamentPlayer.Id != id)
            {
                return NotFound();
            }


            tournamentPlayer.Player = _context.Players.First(a => a.Id.Equals(tournamentPlayer.PlayerId));
            tournamentPlayer.Tournament = _context.Tournaments.First(a => a.Id.Equals(tournamentPlayer.TournamentId));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournamentPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentPlayerExists(tournamentPlayer.PlayerId))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", tournamentPlayer.PlayerId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name", tournamentPlayer.TournamentId);
            return View(tournamentPlayer);
        }

        // GET: TournamentPlayers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentPlayer = await _context.TournamentPlayer
                .Include(t => t.Player)
                .Include(t => t.Tournament)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (tournamentPlayer == null)
            {
                return NotFound();
            }

            return View(tournamentPlayer);
        }

        // POST: TournamentPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tournamentPlayer = await _context.TournamentPlayer.FindAsync(id);
            _context.TournamentPlayer.Remove(tournamentPlayer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentPlayerExists(long id)
        {
            return _context.TournamentPlayer.Any(e => e.PlayerId == id);
        }
    }
}
