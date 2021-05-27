using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly SportsProContext _context;

        public IncidentsController(SportsProContext context)
        {
            _context = context;
        }

        // GET: Incidents
        [Route("/incidents")]
        public async Task<IActionResult> List()
        {
            //get status of filter
            string Filter = HttpContext.Session.GetString("Filter");
            ViewBag.Filter = Filter;

            //var sportsProContext = _context.Incidents.Include(i => i.Customer).Include(i => i.Product).Where(i => i.TechnicianID.Count == 0);
            IQueryable<Incident> sportsProContext = _context.Incidents.Include(i => i.Customer).Include(i => i.Product).Include(i => i.Technician);
            //var incident = from i in _context.Incidents select i;

            //incident = incident.OrderBy(i.)
            if (Filter == "unassigned")
            {
                sportsProContext = sportsProContext.Where(i => i.TechnicianID == null);
            }
            if (Filter == "open")
            {
                sportsProContext = sportsProContext.Where(i => i.DateClosed == null);
            }


            return View(await sportsProContext.ToListAsync());
        }
        //GET: list incidents by technician - generates a dropdown
        public async Task<IActionResult> ListByTech() {              
            ViewData["TechID"] = new SelectList(_context.Technicians, "TechnicianID", "Name");
            return View();
        }
        //POST: list incidents by technician - generates a table of incidents
        [HttpPost]
        public async Task<IActionResult> ListByTech(int id)
        {
            IQueryable<Incident> sportsProContext = _context.Incidents.Include(i => i.Customer).Include(i => i.Product).Include(i => i.Technician).Where(i => i.TechnicianID == id);
            ViewBag.TechincianID = id;
            return View("List",await sportsProContext.ToListAsync());
        }
        //POST: list incidents by technician
        // GET: Incidents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .FirstOrDefaultAsync(m => m.IncidentID == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // GET: Incidents/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");
            ViewData["TechnicianID"] = new SelectList(_context.Technicians, "TechnicianID", "Name");
            return View();
        }

        // POST: Incidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncidentID,CustomerID,ProductID,TechnicianID,Title,Description,DateOpened,DateClosed")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", incident.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", incident.ProductID);
            ViewData["TechnicianID"] = new SelectList(_context.Technicians, "TechnicianID", "Name", incident.TechnicianID);
            return View(incident);
        }

        // GET: Incidents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", incident.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", incident.ProductID);
            ViewData["TechnicianID"] = new SelectList(_context.Technicians, "TechnicianID", "Name", incident.TechnicianID);
            return View(incident);
        }

        // POST: Incidents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncidentID,CustomerID,ProductID,TechnicianID,Title,Description,DateOpened,DateClosed")] Incident incident)
        {
            if (id != incident.IncidentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentExists(incident.IncidentID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", incident.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", incident.ProductID);
            ViewData["TechnicianID"] = new SelectList(_context.Technicians, "TechnicianID", "Name", incident.TechnicianID);
            return View(incident);
        }

        // GET: Incidents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .FirstOrDefaultAsync(m => m.IncidentID == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool IncidentExists(int id)
        {
            return _context.Incidents.Any(e => e.IncidentID == id);
        }

        // runs filters actions
        public IActionResult FilterAll()
        {
            HttpContext.Session.SetString("Filter", "all");
            return RedirectToAction("List");
        }

        public IActionResult FilterUnassigned()
        {
            HttpContext.Session.SetString("Filter", "unassigned");
            return RedirectToAction("List");
        }

        public IActionResult FilterOpen()
        {
            HttpContext.Session.SetString("Filter", "open");
            return RedirectToAction("List");
        }




    }
}
