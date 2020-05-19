using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Taxi.Web.Data.Entities
{
    public class TripEntity
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString ="{0: yyyy:MM:dd}", ApplyFormatInEditMode =false)]
        public DateTime StartDate{ get; set; }

        public DateTime StartDateLocalTime => StartDate.ToLocalTime();

        [DataType(DataType.Date)]
        [Display(Name ="End date")]
        [DisplayFormat(DataFormatString ="{0: yyyy:MM:dd}", ApplyFormatInEditMode =false)]
        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();



    }
}
