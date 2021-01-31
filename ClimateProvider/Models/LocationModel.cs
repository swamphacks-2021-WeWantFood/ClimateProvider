using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateProvider.Models
{
    public record LocationModel
    {
        public string Name { get; set; }
        public double Latitude { get; set }
        public double Longitude { get; set }
    }
}
