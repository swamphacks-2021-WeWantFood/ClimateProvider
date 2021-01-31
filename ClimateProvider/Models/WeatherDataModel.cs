using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateProvider.Models
{
    public class WeatherDataModel
    {
        public string Location { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double AvgTemp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
