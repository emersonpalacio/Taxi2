using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Web.Data.Entities;

namespace Taxi.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTaxisAsync();
        }

        private async Task CheckTaxisAsync()
        {
            if(! _context.Taxis.Any())
            {
                 _context.Taxis.Add(new TaxiEntities { 
                  Plaque="TYD123",
                      Trips= new List<TripEntity> {new TripEntity{ 
                                   StartDate = DateTime.Now,
                                   EndDate = DateTime.UtcNow.AddMinutes(30),
                                   Qualification = 3.5f,
                                   Source = "Robledo",
                                   Target = "mi casa",
                                   Remarks = "Muy vacano e viaje"
                            } ,
                            new TripEntity{
                                   StartDate = DateTime.Now,
                                   EndDate = DateTime.UtcNow.AddMinutes(40),
                                   Qualification = 4.2f,
                                   Source = "CC envigado",
                                   Target = "CC PC",
                                   Remarks = "Regulimbires"
                            }
                       }          
                });

                _context.Taxis.Add(new TaxiEntities { 
                   Plaque ="TRB345",
                   Trips = new List<TripEntity> { new TripEntity{
                                   StartDate = DateTime.Now,
                                   EndDate = DateTime.UtcNow.AddMinutes(60),
                                   Qualification = 4.5f,
                                   Source = "san juan",
                                   Target = "niquia",
                                   Remarks = "taquiado"
                                   },
                                   new TripEntity{
                                   StartDate = DateTime.Now,
                                   EndDate = DateTime.UtcNow.AddMinutes(90),
                                   Qualification = 3.6f,
                                   Source = "centr Medallo",
                                   Target = "Centro niquia",
                                   Remarks = "linda la vieja"
                                   }
                   }
                
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
