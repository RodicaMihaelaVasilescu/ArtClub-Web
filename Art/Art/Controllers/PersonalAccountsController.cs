using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Art.Models;
using Art.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Art.Controllers
{
    public class PersonalAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalAccountsController(ApplicationDbContext context)
        {
            //DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            _context = context;
           // _context = new ApplicationDbContext(options);
            //var context = ServiceLocator.Current.GetInstance<ApplicationDbContext>();
                if (_context.Users.Any())
                {
                    return;   // DB has been seeded
                }
        }

        // GET: PersonalAccounts
        public async Task<IActionResult> Index()
        {
            var clubulPasionatilorDeArtaContext = _context.Users;
            return View(await clubulPasionatilorDeArtaContext.ToListAsync());
        }

        // GET: PersonalAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAccounts = await _context.Users
                .FirstOrDefaultAsync();
            if (personalAccounts == null)
            {
                return NotFound();
            }

            return View(personalAccounts);
        }

        // GET: PersonalAccounts/Create
        public IActionResult Create()
        {
            ViewData["FkGuidUser"] = new SelectList(_context.Users, "GuidUser", "Password");
            return View();
        }

        // POST: PersonalAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPersonalAccount,FkGuidUser,FirstName,LastName,Email,PhoneNumber,Address,City,Country,BirthDate")] PersonalAccounts personalAccounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalAccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkGuidUser"] = new SelectList(_context.Users, "GuidUser", "Password", personalAccounts.FkGuidUser);
            return View(personalAccounts);
        }

        // GET: PersonalAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAccounts = await _context.Users.FindAsync(id);
            if (personalAccounts == null)
            {
                return NotFound();
            }
            //ViewData["FkGuidUser"] = new SelectList(_context.Users, "GuidUser", "Password");
            return View(personalAccounts);
        }

        // POST: PersonalAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPersonalAccount,FkGuidUser,FirstName,LastName,Email,PhoneNumber,Address,City,Country,BirthDate")] PersonalAccounts personalAccounts)
        {
            if (id != personalAccounts.IdPersonalAccount)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalAccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalAccountsExists(personalAccounts.IdPersonalAccount))
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
            ViewData["FkGuidUser"] = new SelectList(_context.Users, "GuidUser", "Password", personalAccounts.FkGuidUser);
            return View(personalAccounts);
        }

        // GET: PersonalAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAccounts = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (personalAccounts == null)
            {
                return NotFound();
            }

            return View(personalAccounts);
        }

        // POST: PersonalAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalAccounts = await _context.Users.FindAsync(id);
            _context.Users.Remove(personalAccounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalAccountsExists(int id)
        {
            return _context.Users.Any(e => e.Id == id.ToString());
        }
    }
}
