using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ClimateProvider.Models;
using ClimateProvider.Services;
using System.Collections.Generic;

namespace ClimateProvider
{
    public static class Functions
    {
        [FunctionName("GetCities")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            INOAAService noaaService = new Services.NOAADummyService();
            CitiesRequest request;
            try
            {
                request = JsonConvert.DeserializeObject<CitiesRequest>(requestBody);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Failed to parse json body: {ex.Message}");
            }

            if(request == null || !IsValidCityRequest(ref request))
            {
                return new BadRequestObjectResult($"Bad request parameters.");
            }

            CitiesResponse response = null;

            var data = await noaaService.GetWeatherData(request.Date, request.Date, request.Temperature.Value, true);

            var resultList = new List<Models.CityData>();
            foreach (var item in data)
            {
                resultList.Add(new CityData()
                {
                    Date = request.Date,
                    HighTemperature = item.MaxTemp,
                    Latitude = item.Latitude.ToString(),
                    Longitude = item.Longitude.ToString(),
                    LowTemperature = item.MinTemp,
                    Name = item.Location,
                });
            }
            response.Cities = resultList;

            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        private static bool IsValidCityRequest(ref CitiesRequest request)
        {
            if (!request.Temperature.HasValue)
            {
                return false;
            }
            if (request.Scale == TemperatureScales.NotSpecified)
            {
                request.Scale = TemperatureScales.Fahrenheit;
            }
            return true;
        }
    }
}
