using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Art.Models;
using System;

namespace Art.Controllers
{
    public class EventsController : Controller
    {
        private ClubulPasionatilorDeArtaContext _context;

        public EventsController()
        {
            _context = new ClubulPasionatilorDeArtaContext();
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.IdEvent == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEvent,EventName,Place,StartDate,EndDate,Tag")] Events events)
        {
            int idEvent = 0;
            if (_context.Events.Any())
            {
                idEvent = _context.Events.Max(e => e.IdEvent) + 1;
            }
            events.IdEvent = idEvent;
            if (ModelState.IsValid)
            {
                _context.Add(events);
                int id = 0;
                if (_context.Invoice.Any())
                {
                    id = _context.Invoice.Max(e => e.IdInvoice) + 1;
                }
                _context.Invoice.Add(new Invoice
                {
                    Cost = 30,
                    FkIdEvent = events.IdEvent,
                    FkGuidUser = Guid.Parse("32055087-8903-4AF8-BC13-448BFEA38CA4"),
                    IdInvoice = id,
                    InvoiceDate = DateTime.Now
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEvent,EventName,Place,StartDate,EndDate,Tag")] Events events)
        {
            if (id != events.IdEvent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.IdEvent))
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
            return View(events);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoice.FirstOrDefault(i=>i.FkIdEvent == id);
            if(invoice != null)
            {
                _context.Invoice.Remove(invoice);
                _context.SaveChanges();
            }
            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.IdEvent == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var events = await _context.Events.FindAsync(id);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.IdEvent == id);
        }
    }
}
