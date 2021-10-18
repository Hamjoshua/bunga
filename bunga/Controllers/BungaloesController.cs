using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bunga.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace bunga.Models
{
    public class BungaloesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BungaloesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bungaloes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bungalo.ToListAsync());
        }

        // GET: Bungaloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bungalo = await _context.Bungalo
                .FirstOrDefaultAsync(m => m.Id_бунгало == id);
            if (bungalo == null)
            {
                return NotFound();
            }

            return View(bungalo);
        }

        [Authorize(Roles = RoleNames.Deliver)]
        // GET: Bungaloes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bungaloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_бунгало,Адрес,Количество_челоек,Wi_fi,Id_сдающий,Стоимость_сутки")] Bungalo bungalo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bungalo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bungalo);
        }

        [Authorize(Roles = RoleNames.Deliver)]
        // GET: Bungaloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bungalo = await _context.Bungalo.FindAsync(id);
            if (bungalo == null)
            {
                return NotFound();
            }
            if (bungalo.Сдающий.UserName == User.Identity.Name)
            {                
                return NotFound();
            }
            return View(bungalo);
        }

        // POST: Bungaloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleNames.Deliver)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_бунгало,Адрес,Количество_челоек,Wi_fi,Id_сдающий,Стоимость_сутки")] Bungalo bungalo)
        {
            if (id != bungalo.Id_бунгало)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bungalo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BungaloExists(bungalo.Id_бунгало))
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
            return View(bungalo);
        }

        // GET: Bungaloes/Delete/5
        
        [Authorize(Roles = RoleNames.EditorsOfBungalo)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bungalo = await _context.Bungalo
                .FirstOrDefaultAsync(m => m.Id_бунгало == id);
            if (bungalo == null)
            {
                return NotFound();
            }

            return View(bungalo);
        }

        // POST: Bungaloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = RoleNames.EditorsOfBungalo)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bungalo = await _context.Bungalo.FindAsync(id);
            _context.Bungalo.Remove(bungalo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BungaloExists(int id)
        {
            return _context.Bungalo.Any(e => e.Id_бунгало == id);
        }
    }
}
