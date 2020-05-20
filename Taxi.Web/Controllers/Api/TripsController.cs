using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taxi.Web.Data;
using Taxi.Web.Helpers;
using Taxi.Common.Enum;
using Taxi.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Taxi.Web.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelpers _userHelpers;
        private readonly IConverterHelper _converterHelper;

        public TripsController(DataContext context,
                               IUserHelpers userHelpers,
                               IConverterHelper converterHelper  )
        {
            this._context = context;
            this._userHelpers = userHelpers;
            this._converterHelper = converterHelper; 
        }

        
        // POST: api/Trips
        [HttpPost]
        public async Task<IActionResult> PostTripEntity([FromBody] TripRequest tripReques)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserEntity userEntity = await _userHelpers.GetUserAsync(tripReques.UserId);

            if (userEntity == null)
            {
                return BadRequest("User dot not exist. ");
            }

            TaxiEntities taxiEntities = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == tripReques.Plaque);
            if (taxiEntities == null)
            {
                _context.Taxis.Add(new TaxiEntities { Plaque = tripReques.Plaque.ToUpper() });
                await _context.SaveChangesAsync();
                taxiEntities = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == tripReques.Plaque);
            }

            TripEntity tripEntity = new TripEntity {
                Source = tripReques.Address,
                SourceLatitude = tripReques.Latitude,
                SourceLongitude = tripReques.Longitude,
                StartDate = DateTime.UtcNow,
                Taxi = taxiEntities,
                TripDetails = new List<TripDetailEntity> { new TripDetailEntity {
                    Date = DateTime.UtcNow,
                    Latitude = tripReques.Latitude,
                    Longitude = tripReques.Longitude
                   }
                },
                User = userEntity,
            };
            _context.Trips.Add(tripEntity);

            return Ok(_converterHelper.ToTripResponse(tripEntity));          

        }

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
