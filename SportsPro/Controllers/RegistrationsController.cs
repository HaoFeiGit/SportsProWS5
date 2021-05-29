using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly SportsProContext _context;

        public RegistrationsController(SportsProContext context)
        {
            _context = context;
        }

        // GET: Registrations
        [Route("/registrations")]
        public async Task<IActionResult> List()
        {
            var sportsProContext = _context.Registrations.Include(r => r.Customer).Include(r => r.Product);
            return View(await sportsProContext.ToListAsync());
        }
        //GET - list all customers for a dropdown
        public async Task<IActionResult> GetCustomer()
        {
            //    CustomersViewModel locationsHistoryVM = new InventoryLocationsHistoryViewModel();
            //    var locations = new SelectList(_context.InventoryLocations.OrderBy(l => l.LocationName)
            //    .ToDictionary(us => us.LocationId, us => us.LocationName), "Key", "Value");
            //    ViewBag.Locations = locations;
            //https://stackoverflow.com/questions/49451785/cannot-implicitly-convert-type-system-collections-generic-listinventorylocatio
            //return View(locationsHistoryVM);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
            return View();
        }
        //POST: list product registrations by customer - generates a table of registered products
        [HttpPost]
        public async Task<IActionResult> GetCustomer(int id)
        {
            IQueryable<Registration> sportsProContext = _context.Registrations.Include(i => i.Customer).Include(i => i.Product).Where(i => i.CustomerID == id);
            TempData["Cust"] = sportsProContext.Select(t => t.Customer.FullName).FirstOrDefault();
            return View("List", await sportsProContext.ToListAsync());
        }
        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.RegistrationID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationID,CustomerID,ProductID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", registration.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", registration.ProductID);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", registration.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", registration.ProductID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationID,CustomerID,ProductID")] Registration registration)
        {
            if (id != registration.RegistrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", registration.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", registration.ProductID);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.RegistrationID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationID == id);
        }
    }
}
