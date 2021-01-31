using ClimateProvider.Models;
using ClimateProvider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateProvider.Services
{
    class NOAADummyService : Services.INOAAService
    {
        public async Task<IEnumerable<WeatherDataModel>> GetWeatherData(DateTime StartDate, DateTime EndDate, double DesiredTemperature, bool OnlyUnitedStates)
        {
            List<String> locationNames = new List<String>();

            locationNames.Add("New York City, NY US");
            locationNames.Add("Albequerque, NM US");
            locationNames.Add("Tucson, AZ US");
            locationNames.Add("Tallahassee, FL US");
            locationNames.Add("Tampa, FL US");
            locationNames.Add("Buffalo, NY US");
            locationNames.Add("Milwaukee, WI US");
            locationNames.Add("Los Angeles, CA US");
            locationNames.Add("Austin, TX US");
            locationNames.Add("Dallas, TX US");
            locationNames.Add("Atlanta, GA US");
            locationNames.Add("Savannah, GA US");
            locationNames.Add("Montgomery, AL US");
            locationNames.Add("Portland, OR US");
            locationNames.Add("Seattle, WA US");
            locationNames.Add("Pittsburgh, PA US");
            locationNames.Add("Asheville, NC US");
            locationNames.Add("Raleigh, NC US");
            locationNames.Add("Knoxville, TN US");
            locationNames.Add("Little Rock, AR US");
            locationNames.Add("Reno, NV US");
            locationNames.Add("Las Vegas, NV US");
            locationNames.Add("Richmond, VA US");

            if(!OnlyUnitedStates)
            {
                locationNames.Add("London, UK");
                locationNames.Add("Brighton, UK");
                locationNames.Add("Hamburg, DE");
                locationNames.Add("Berlin, DE");
                locationNames.Add("Amsterdam, NL");
                locationNames.Add("Prague, CZ");
                locationNames.Add("Cape Town, SA");
                locationNames.Add("Sydney, AU");
                locationNames.Add("Melbourne, AU");
                locationNames.Add("Ontario, CA");
                locationNames.Add("Toronto, CA");
                locationNames.Add("Mexico City, MX");
                locationNames.Add("Madrid, ES");
                locationNames.Add("Paris, FR");
                locationNames.Add("Barcelona, ES");
                locationNames.Add("Beijing, CN");
                locationNames.Add("Seoul, KR");
                locationNames.Add("Moscow, RU");
            }

            List<WeatherDataModel> dataModels = new List<WeatherDataModel>();
            Random r = new Random();

            for(int i = 0; i < 6; i++)
            {
                WeatherDataModel model = new WeatherDataModel();
                model.Location = locationNames.ElementAt(r.Next(locationNames.Count));
                model.MaxTemp = 55.0 + (r.NextDouble() * 35.0);
                model.MinTemp = 0.0 + (r.NextDouble() * 35.0);
                model.AvgTemp = Math.Abs(model.MaxTemp - model.MinTemp) / 2.0;
                model.Latitude = 38.9;
                model.Longitude = -77.04;
                dataModels.Add(model);
            }

            return dataModels;
        }
    }
}
