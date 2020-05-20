using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Taxi.Web.Data;
using Taxi.Web.Data.Entities;

namespace Taxi.Web.Controllers
{
    [Authorize(Roles ="Admin")]
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
            return View(await _context.Taxis.OrderBy(t => t.Plaque).ToListAsync());
        }

        // GET: TaxiEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Taxis.FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: TaxiEntities/Create
        public IActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( TaxiEntities model)
        {
            if (ModelState.IsValid)
            {
                model.Plaque = model.Plaque.ToUpper();
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: TaxiEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Taxis.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaxiEntities model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                model.Plaque = model.Plaque.ToUpper();
                _context.Update(model);
               await _context.SaveChangesAsync();         
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: TaxiEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Taxis .FirstOrDefaultAsync(t => t.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            
            _context.Taxis.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  
        }

           
        private bool TaxiEntitiesExists(int id)
        {
            return _context.Taxis.Any(e => e.Id == id);
        }
    }
}
