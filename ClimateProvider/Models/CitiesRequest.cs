using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateProvider.Models
{
    public enum TemperatureScales
    {
        NotSpecified = 0,
        Fahrenheit,
        Celcius,
    }
    class CitiesRequest
    {
        public double? Temperature { get; set; }
        public TemperatureScales Scale { get; set; }
        public DateTime Date { get; set; }
    }
}
