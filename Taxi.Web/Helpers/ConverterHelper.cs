﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Common.Enum;
using Taxi.Web.Data;
using Taxi.Web.Data.Entities;

namespace Taxi.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;

        public ConverterHelper(DataContext context)
        {
            _context = context;
        }

        public TaxiResponse ToTaxiResponse(TaxiEntities taxiEntity)
        {
            return new TaxiResponse {
                Id = taxiEntity.Id,
                Plaque = taxiEntity.Plaque,
                Trips = taxiEntity.Trips?.Select(t  => new TripResponse { 
                  EndDate =t.EndDate,
                  Id = t.Id,
                  Qualification = t.Qualification,
                  Remarks= t.Remarks,
                  Source = t.Source,
                  SourceLatitude =t.SourceLatitude,
                  SourceLongitude= t.SourceLongitude,
                  StartDate =t.StartDate,
                  Target = t.Target,
                  TargetLatitude =t.TargetLatitude,
                  TargetLongitude = t.TargetLongitude,
                      TripDetails = t.TripDetails?.Select(td => new TripDetailResponse { 
                        Date = td.Date,
                        Id =td.Id,
                        Latitude = td.Latitude,
                        Longitude = td.Longitude
                      }).ToList(),
                  User = ToUserResponse(t.User)                
                }).ToList(),                
            };
        }

        private UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
                return null;
            return new UserResponse { 
               Address =user.Address,
               Document =user.Document,
               Email =  user.Email,
               FirstName = user.FirstName,
               Id = user.Id,
               LastName = user.LastName,
               PhoneNumber = user.PhoneNumber,
               PicturePath  = user.PicturePath,
               UserType = user.UserType
               
            };
        }
    }
}
