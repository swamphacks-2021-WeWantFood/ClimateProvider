using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateProvider.Services
{
    interface INOAAService
    {
        public IEnumerable<Models.WeatherDataModel> GetWeatherData(DateTime StartDate, DateTime EndDate, double DesiredTemperature, bool OnlyUnitedStates);
    }
}
