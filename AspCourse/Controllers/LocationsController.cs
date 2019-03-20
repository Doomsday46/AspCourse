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
    public class LocationsController : Controller
    {
        private readonly ApplicationContext _context;

        public LocationsController(ApplicationContext context)
        {
            _context = context;
        }

        private void initDataView()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";
        }

        // GET: Locations
        [Authorize]
        public async Task<IActionResult> Index()
        {
            initDataView();
            return View(await _context.Locations.Where(a => a.UserId != null && a.UserId.Equals(GetIdUser())).ToListAsync());
        }

        // GET: Locations/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId.Equals(GetIdUser()));
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        [Authorize]
        public IActionResult Create()
        {
            initDataView();
            return View();
        }

        private int GetIdUser() {
            try
            {
                return _context.Users.First(a => a.Email.Equals(HttpContext.User.Identity.Name)).Id;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Location location)
        {
            initDataView();
            location.UserId = GetIdUser();
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Location location)
        {
            initDataView();
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
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
            return View(location);
        }

        // GET: Locations/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            initDataView();
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            initDataView();
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}
