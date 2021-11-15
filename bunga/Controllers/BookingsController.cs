using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bunga.Data;
using bunga.Models;
using Microsoft.AspNetCore.Identity;

namespace bunga.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var booking_sell = await _context.Booking.Include(b => b.Арендатор).Include(b => b.Бунгало).Include(bb => bb.Бунгало.Сдающий)
                .Where(b => b.Бунгало.Сдающий.UserName == User.Identity.Name).ToListAsync();
            ViewBag.booking_sell = booking_sell;
            return View(await _context.Booking.Include(b => b.Арендатор).Include(b => b.Бунгало).Include(bb => bb.Бунгало.Сдающий)
                .Where(b => b.Арендатор.UserName == User.Identity.Name).ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.Id_бронь == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create/5
        public IActionResult Create(int bungalo_id)
        {
            var bungalo = _context.Bungalo.Where(b => b.Id_бунгало == bungalo_id).First();
            ViewBag.bungalo = bungalo;
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_бронь,Дата_начала,Дата_конца")] Booking booking, int bungalo_id)
        {
            var bungalo = _context.Bungalo.Where(b => b.Id_бунгало == bungalo_id).First();
            ViewBag.bungalo = bungalo;
            if (ModelState.IsValid && booking.Дата_конца > booking.Дата_начала)
            {
                var user = await _userManager.GetUserAsync(User);
                booking.Арендатор = user;
                booking.Бунгало = bungalo;
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (booking.Дата_конца <= booking.Дата_начала)
                ModelState.AddModelError("Дата_конца", "Дата окончания бронирования должна быть больше, чем дата начала бронирования");
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_бронь,Дата_начала,Дата_конца")] Booking booking)
        {
            if (id != booking.Id_бронь)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id_бронь))
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
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.Id_бронь == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id_бронь == id);
        }
    }
}
