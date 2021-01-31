using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateProvider.Models
{
    class CityData
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public DateTime Date { get; set; }

        public double LowTemperature { get; set; }
        public double HighTemperature { get; set; }
    }
}
