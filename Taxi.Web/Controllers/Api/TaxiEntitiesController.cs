using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi.Web.Data;
using Taxi.Web.Data.Entities;
using Taxi.Web.Helpers;

namespace Taxi.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiEntitiesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TaxiEntitiesController(DataContext context,
                                     IConverterHelper converterHelper)
        {
            _context = context;
            this._converterHelper = converterHelper;
        }

        [HttpGet("{Plaque}")]
        public async Task<IActionResult> GetTaxiEntities([FromRoute] string plaque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            plaque = plaque.ToUpper();
            TaxiEntities taxiEntities = await _context.Taxis
                               .Include(t => t.User )
                               .Include(t => t.Trips)
                               .ThenInclude(t => t.TripDetails)
                               .Include(t => t.Trips)
                               .ThenInclude(tr=> tr.User )
                               .FirstOrDefaultAsync(t => t.Plaque == plaque);

            if (taxiEntities == null)
            {
                _context.Taxis.Add(new TaxiEntities { 
                  Plaque =plaque
                });
                await _context.SaveChangesAsync();
                taxiEntities = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == plaque);
            }

            return Ok(_converterHelper.ToTaxiResponse(taxiEntities));
        }           
    }
}