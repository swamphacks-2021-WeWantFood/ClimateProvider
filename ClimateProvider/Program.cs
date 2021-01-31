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

namespace ClimateProvider
{
    /*
     * Okay, so.. In c#, we don't have pointers obviously.
     * And it's unlike Java in that not everything is automatically passed by-reference.
     *
     * There are two main *types* of data, **Structs* and **Classes**
     *
     * The difference being that structs are valuetypes - pass by-value, and classes are rerference types - pass by-reference.
     *
     * That's prboably the largest difference from c++ in terms of the language itself.
     *  Obviously the libraries are different, but otherwise the syntax itself should be basically the same.
     *
     * C# does actually technically have pointers, but there's no _point_ (pun intended) in using them in most cases.
     */
    public static class Program
    {

        // I'm not actually sure if c++ has these, but these are attributes (decorators in java iirc)
        // Basically they provide metadata of some kind on the level of classes/methods/functions/properties/etc.
        // This specifically tells azure that this function should run when the user navigates to "ourwebsite/GetCities"
        [FunctionName("GetCities")]
        public static async Task<IActionResult> RunCities(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)//, INOAAService noaaService)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            CitiesRequest request;
            try
            {
                request = JsonConvert.DeserializeObject<CitiesRequest>(requestBody);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Failed to parse json body: {ex.Message}");
            }

            if(!IsValidCityRequest(ref request))
            {
                return new BadRequestObjectResult($"Bad request parameters.");
            }


            //  We'll poll the noaa service for data, do any kind of transformations necessary on it
            // and spit it back out here.

            return new OkResult();
        }

        private static bool IsValidCityRequest(ref CitiesRequest request)
        {
            if(request.Month < 1 || request.Month > 12)
            {
                return false;
            }
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
