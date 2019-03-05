using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspCourse.Models;

namespace AspCourse.Controllers
{
    public class TournamentLocationsController : Controller
    {
        private readonly ApplicationContext _context;

        public TournamentLocationsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: TournamentLocations
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.TournamentLocation.Include(t => t.Location).Include(t => t.Tournament);
            return View(await applicationContext.ToListAsync());
        }

        // GET: TournamentLocations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentLocation = await _context.TournamentLocation
                .Include(t => t.Location)
                .Include(t => t.Tournament)
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (tournamentLocation == null)
            {
                return NotFound();
            }

            return View(tournamentLocation);
        }

        // GET: TournamentLocations/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id");
            return View();
        }

        // POST: TournamentLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,TournamentId")] TournamentLocation tournamentLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournamentLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", tournamentLocation.LocationId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", tournamentLocation.TournamentId);
            return View(tournamentLocation);
        }

        // GET: TournamentLocations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentLocation = await _context.TournamentLocation.FindAsync(id);
            if (tournamentLocation == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", tournamentLocation.LocationId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", tournamentLocation.TournamentId);
            return View(tournamentLocation);
        }

        // POST: TournamentLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("LocationId,TournamentId")] TournamentLocation tournamentLocation)
        {
            if (id != tournamentLocation.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournamentLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentLocationExists(tournamentLocation.LocationId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", tournamentLocation.LocationId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", tournamentLocation.TournamentId);
            return View(tournamentLocation);
        }

        // GET: TournamentLocations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentLocation = await _context.TournamentLocation
                .Include(t => t.Location)
                .Include(t => t.Tournament)
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (tournamentLocation == null)
            {
                return NotFound();
            }

            return View(tournamentLocation);
        }

        // POST: TournamentLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tournamentLocation = await _context.TournamentLocation.FindAsync(id);
            _context.TournamentLocation.Remove(tournamentLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentLocationExists(long id)
        {
            return _context.TournamentLocation.Any(e => e.LocationId == id);
        }
    }
}
