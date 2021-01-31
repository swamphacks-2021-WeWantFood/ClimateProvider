using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClimateProvider.Services
{
    interface INOAAService
    {
        public Task<IEnumerable<Models.WeatherDataModel>> GetWeatherData(DateTime StartDate, DateTime EndDate, double DesiredTemperature, bool OnlyUnitedStates);
    }
}
