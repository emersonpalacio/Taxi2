using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Taxi.Web.Data;
using Taxi.Web.Data.Entities;

namespace Taxi.Web.Controllers
{
    public class TaxiEntitiesController : Controller
    {
        private readonly DataContext _context;

        public TaxiEntitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: TaxiEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Taxis.ToListAsync());
        }

        // GET: TaxiEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiEntities = await _context.Taxis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxiEntities == null)
            {
                return NotFound();
            }

            return View(taxiEntities);
        }

        // GET: TaxiEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaxiEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Plaque")] TaxiEntities taxiEntities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxiEntities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxiEntities);
        }

        // GET: TaxiEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiEntities = await _context.Taxis.FindAsync(id);
            if (taxiEntities == null)
            {
                return NotFound();
            }
            return View(taxiEntities);
        }

        // POST: TaxiEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Plaque")] TaxiEntities taxiEntities)
        {
            if (id != taxiEntities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxiEntities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxiEntitiesExists(taxiEntities.Id))
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
            return View(taxiEntities);
        }

        // GET: TaxiEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiEntities = await _context.Taxis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxiEntities == null)
            {
                return NotFound();
            }

            return View(taxiEntities);
        }

        // POST: TaxiEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxiEntities = await _context.Taxis.FindAsync(id);
            _context.Taxis.Remove(taxiEntities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxiEntitiesExists(int id)
        {
            return _context.Taxis.Any(e => e.Id == id);
        }
    }
}
