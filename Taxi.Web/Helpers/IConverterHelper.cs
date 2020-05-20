using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Common.Enum;
using Taxi.Web.Data.Entities;


namespace Taxi.Web.Helpers
{
    public interface IConverterHelper
    {
        TaxiResponse ToTaxiResponse(TaxiEntities taxiEntity);
    }
}
