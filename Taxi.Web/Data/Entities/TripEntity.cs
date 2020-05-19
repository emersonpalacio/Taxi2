using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Web.Data.Entities
{
    public class TripEntity
    {
        public int Id { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Start day ")]
        [DisplayFormat(DataFormatString ="{0: yyyy:MM:dd hh:mm}", ApplyFormatInEditMode =false)]
        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        [DataType(DataType.DateTime)]
        [Display(Name ="End Day")]
        [DisplayFormat(DataFormatString ="{0: yyyy:MM:dd hh:mm}", ApplyFormatInEditMode =false)]
        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        [MaxLength(500, ErrorMessage = "The {0} field must have {1} characters.")]
        public string  Source { get; set; }

        [MaxLength(500,ErrorMessage = "The {0} field must have {1} characters.")]
        public string Target { get; set; }

        public float Qualification { get; set; }
        public double SourceLatitude { get; set; }
        public double SourceLongitude { get; set; }
        public double TargetLatitude { get; set; }
        public double TargetLogitude { get; set; }
        public string Remarks { get; set; }

        public TaxiEntities  Taxi { get; set; }
        public ICollection<TripDetailEntity> TripDetails { get; set; }

    }
}
