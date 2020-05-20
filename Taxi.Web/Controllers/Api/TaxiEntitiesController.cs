using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi.Web.Data;
using Taxi.Web.Data.Entities;

namespace Taxi.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiEntitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public TaxiEntitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{plaque}")]
        public async Task<IActionResult> GetTaxiEntities([FromRoute] string plaque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            plaque = plaque.ToUpper();
            TaxiEntities taxiEntities = await _context.Taxis
                               .Include(t => t.Trips)
                               .FirstOrDefaultAsync(t => t.Plaque == plaque);

            if (taxiEntities == null)
            {
                _context.Taxis.Add(new TaxiEntities { 
                  Plaque =plaque
                });

                await _context.SaveChangesAsync();
                taxiEntities = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == plaque);
            }

            return Ok(taxiEntities);
        }   

        
    }
}