using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateProvider.Models
{   
    public record TemperatureValueInfoModel
    {
        /// <summary>
        /// Get or Set the Station ID of this object
        /// </summary>
        public string Station { get; set; }
        /// <summary>
        /// Get or Set the Name of the Value category (i.e. TMAX or TMIN for max or min temp, respectively)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or Set the actual Value of the temperature reading
        /// </summary>
        public double Value { get; set; }
    }
}
