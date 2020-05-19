using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Web.Data.Entities
{
    public class TripDetailEntity
    {
        public int Id { get; set; }

        [Display(Name ="Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:}")]
        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public TripEntity Trip { get; set; }
    }
}
