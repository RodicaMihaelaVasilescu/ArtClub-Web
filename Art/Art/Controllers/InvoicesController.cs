using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Art.Models;

namespace Art.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ClubulPasionatilorDeArtaContext _context;

        public InvoicesController()
        {
            _context = new ClubulPasionatilorDeArtaContext();
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var clubulPasionatilorDeArtaContext = _context.Invoice.Include(i => i.FkGuidUserNavigation).Include(i => i.FkIdEventNavigation);
            return View(await clubulPasionatilorDeArtaContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.FkGuidUserNavigation)
                .Include(i => i.FkIdEventNavigation)
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["FkGuidUser"] = new SelectList(_context.Users, "GuidUser", "Password");
            ViewData["FkIdEvent"] = new SelectList(_context.Events, "IdEvent", "EventName");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInvoice,FkGuidUser,FkIdEvent,Cost,InvoiceDate")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkGuidUser"] = new SelectList(_context.Users, "GuidUser", "Password", invoice.FkGuidUser);
            ViewData["FkIdEvent"] = new SelectList(_context.Events, "IdEvent", "EventName", invoice.FkIdEvent);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["FkGuidUser"] = new SelectList(_context.Users, "GuidUser", "Password", invoice.FkGuidUser);
            ViewData["FkIdEvent"] = new SelectList(_context.Events, "IdEvent", "EventName", invoice.FkIdEvent);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInvoice,FkGuidUser,FkIdEvent,Cost,InvoiceDate")] Invoice invoice)
        {
            if (id != invoice.IdInvoice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.IdInvoice))
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
            ViewData["FkGuidUser"] = new SelectList(_context.Users, "GuidUser", "Password", invoice.FkGuidUser);
            ViewData["FkIdEvent"] = new SelectList(_context.Events, "IdEvent", "EventName", invoice.FkIdEvent);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.FkGuidUserNavigation)
                .Include(i => i.FkIdEventNavigation)
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice.Any(e => e.IdInvoice == id);
        }
    }
}
