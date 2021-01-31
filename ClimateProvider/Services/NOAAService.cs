using ClimateProvider.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClimateProvider.Services
{
    class MetaData
    {
        public IEnumerable<TemperatureValueInfoModel> Results { get; set; }
    }

    class NOAAService : Services.INOAAService
    {
        private const string URL = "https://www.ncdc.noaa.gov/cdo-web/api/v2/";
        private readonly IConfiguration config;

        public NOAAService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<IEnumerable<WeatherDataModel>> GetWeatherData(DateTime StartDate, DateTime EndDate, double DesiredTemperature, bool OnlyUnitedStates)
        {
            List<WeatherDataModel> dataModels = new List<WeatherDataModel>();

            string firstApiCallParameters = "data?datasetid=GSOM&datatypeid=TMIN&datatypeid=TMAX&startdate=&enddate=2012-09-10&units=standard&locationid=FIPS:US";
            const string secondApiCallParameters = "stations/";
            string authToken = config["apiAuthKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Add("token", authToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                MetaData metaData;
                StationInfoModel stationInfoModel;

                double closestAverageTemperatureToGoal = 9999.0;
                double currentMaxTemperature = 0.0;
                double currentAverageTemperature = 0.0;

                HttpResponseMessage response = await client.GetAsync(firstApiCallParameters);
                if(response.IsSuccessStatusCode)
                {
                    metaData = await response.Content.ReadAsAsync<MetaData>();

                    foreach(TemperatureValueInfoModel info in metaData.Results)
                    {
                        response = await client.GetAsync(secondApiCallParameters + info.Station);
                        if(response.IsSuccessStatusCode)
                        {
                            stationInfoModel = await response.Content.ReadAsAsync<StationInfoModel>();

                            if (stationInfoModel.ID.Equals(info.Station))
                            {
                                if (info.Name.Contains("TMAX"))
                                {
                                    currentMaxTemperature = info.Value;
                                }
                                else
                                {
                                    currentAverageTemperature = (info.Value + currentMaxTemperature) / 2;

                                    if (Math.Abs(DesiredTemperature - currentAverageTemperature) < closestAverageTemperatureToGoal)
                                    {
                                        closestAverageTemperatureToGoal = Math.Abs(DesiredTemperature - currentAverageTemperature);
                                        WeatherDataModel model = new WeatherDataModel();
                                        model.AvgTemp = currentAverageTemperature;
                                        model.MaxTemp = currentMaxTemperature;
                                        model.MinTemp = info.Value;
                                        model.Location = stationInfoModel.Name;
                                        model.Latitude = stationInfoModel.Latitude;
                                        model.Longitude = stationInfoModel.Longitude;
                                        dataModels.Add(model);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return dataModels;
        }
    }
}
