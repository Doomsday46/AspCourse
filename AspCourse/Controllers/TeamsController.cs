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
    public class TeamsController : Controller
    {
        private readonly ApplicationContext _context;

        public TeamsController(ApplicationContext context)
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

        // GET: Teams
        [Authorize]
        public async Task<IActionResult> Index()
        {
            initDataView();
            var applicationContext = _context.Teams.Include(t => t.Tournament).Include(t => t.User).Where(a => a.UserId.Equals(GetIdUser()));
            return View(await applicationContext.ToListAsync());
        }

        // GET: Teams/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Tournament)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId.Equals(GetIdUser()));
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        [Authorize]
        public IActionResult Create()
        {
            initDataView();
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,UserId,TournamentId")] Team team)
        {
            initDataView();
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", team.TournamentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", team.UserId);
            return View(team);
        }

        // GET: Teams/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", team.TournamentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", team.UserId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId,TournamentId")] Team team)
        {
            initDataView();
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", team.TournamentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", team.UserId);
            return View(team);
        }

        // GET: Teams/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Tournament)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            initDataView();
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowPlayer(int id)
        {
            try
            {
                initDataView();
                var team = _context.Teams.Include(t => t.Players).FirstOrDefault(a => a.Id.Equals(id) && a.UserId != null && a.UserId.Equals(GetIdUser()));

                if(team.Players.Count == 0) return View(new List<Player>());
                return View(team.Players);
            }
            catch (Exception)
            {
                return View(new List<Player>());
            }
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
