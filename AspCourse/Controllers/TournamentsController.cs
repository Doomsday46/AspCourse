﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspCourse.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspCourse.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ApplicationContext _context;

        public TournamentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Tournaments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tournaments.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
        }

        private long GetIdUser()
        {
            return _context.Users.FirstOrDefault(a => a.Email.Equals(HttpContext.User.Identity.Name)).Id;
        }

        // GET: Tournaments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(long? id)
        {
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

            return View(tournament);
        }

        // GET: Tournaments/Create
        [Authorize]
        public IActionResult Create()
        {
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
        public async Task<IActionResult> Edit(long? id)
        {
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,DateTime")] Tournament tournament)
        {
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

        // GET: Tournaments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
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
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentExists(long id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }
    }
}
