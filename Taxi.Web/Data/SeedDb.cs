using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Common.Enum;
using Taxi.Web.Data.Entities;
using Taxi.Web.Helpers;

namespace Taxi.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelpers _userHelpers;

        public SeedDb(DataContext context,
                    IUserHelpers userHelpers)
        {
            _context = context;
            this._userHelpers = userHelpers;
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTaxisAsync();
            await CheckRolesAsync();
            var admin = await CheckUserAsync("1010", "Emerson", "Palacio", "emersonpalaciootalvaro@gmail.com", "350 634 2747", "Cll34#56-6", UserType.Admin);
            var driver = await CheckUserAsync("2020", "sara", "palacio", "sara@yopmail.com", "350 634 2747", "Cll56#45-67", UserType.Driver);
            var user1 = await CheckUserAsync("3030", "emmanuel", "palacio", "emmanuel@yopmail.com", "350 634 2747", "Crr67#45-78", UserType.User);
            var user2 = await CheckUserAsync("4040", "rubiela", "gaviria", "rubiela@yopmail.com", "350 634 2747", "Crr45#78-9", UserType.User);

        }

       

        private async Task CheckTaxisAsync()
        {
            if(! _context.Taxis.Any())
            {
                 _context.Taxis.Add(new TaxiEntities { 
                  Plaque="TYD123",
                      Trips= new List<TripEntity> {new TripEntity{ 
                                   StartDate = DateTime.UtcNow,
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

        private async Task CheckRolesAsync()
        {
            await _userHelpers.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelpers.CheckRoleAsync(UserType.Driver.ToString());
            await _userHelpers.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<UserEntity> CheckUserAsync(string document, string firstName, string lastName, string email,
                                                                                        string phone, string address, UserType userType)

        {
            var user = await _userHelpers.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelpers.AddUserAsync(user,"123456");
                await _userHelpers.AddUserToRoleAsync(user, userType.ToString());

            }

            return user;

        }
    }
}
