using System;
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
    public class PlayersController : Controller
    {
        private readonly ApplicationContext _context;

        public PlayersController(ApplicationContext context)
        {
            _context = context;
        }

        private void initDataView()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";
        }

        // GET: Players
        [Authorize]
        public async Task<IActionResult> Index()
        {
            initDataView();
            return View(await _context.Players.Where(m => m.UserId != null && m.UserId.Equals(GetIdUser())).ToListAsync());
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

        // GET: Players/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }
            
            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId.Equals(GetIdUser()));
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        [Authorize]
        public IActionResult Create()
        {
            initDataView();
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SecondName,BirthDay")] Player player)
        {
            initDataView();
            player.UserId = GetIdUser();
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SecondName,BirthDay")] Player player)
        {
            initDataView();
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            return View(player);
        }

        // GET: Players/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            initDataView();
            var player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
