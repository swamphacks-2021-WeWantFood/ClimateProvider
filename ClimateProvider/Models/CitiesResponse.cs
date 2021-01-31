using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateProvider.Models
{
    class CitiesResponse
    {
        public IEnumerable<CityData> Cities { get; set; }
    }
}
