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
            if (! _context.Taxis.Any())
            {
                _context.Taxis.Add(new TaxiEntities
                {
                    Plaque = "TPC123",
                    Trip = new List<TripEntity> { new TripEntity{
                          StartDate = DateTime.UtcNow,
                          EndDate = DateTime.UtcNow.AddMinutes(30),
                          Qualification = 4.5f,
                          Source = "ITM Fraterenidad",
                          Target ="ITM Robledo",
                          Remarks = "Quede iniciado",                         
                          
                         },
                         new TripEntity{ 
                          StartDate = DateTime.UtcNow,
                          EndDate = DateTime.UtcNow.AddMinutes(45),
                          Qualification = 4.3f,
                          Source = "MI housue",
                          Target = "CC Viva envigado",
                          Remarks ="La conducta es muy linda"

                         }
                    }
                });

                _context.Taxis.Add(new TaxiEntities { 
                    Plaque = "FBY555",

                    Trip = new List<TripEntity> { new TripEntity { 
                        StartDate = DateTime.UtcNow,
                        EndDate  = DateTime.UtcNow.AddMinutes(55),
                        Qualification = 3.6f,
                        Source = "CC viva",
                        Target = "MI house",
                        Remarks = "Maso la vieja muy bonita pero muy seria"                   
                      },
                      new TripEntity{ 
                          StartDate = DateTime.UtcNow,
                          EndDate = DateTime.UtcNow.AddMinutes(25),
                          Qualification = 4.7f,
                          Source = "centro medellin",
                          Target = "CC viva envigado",
                          Remarks = "chimba de servicio"                          
                      }                    
                    }              
                  
                });

                await _context.SaveChangesAsync();
            
            }
        }
    }
}
