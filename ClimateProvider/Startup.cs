using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(ClimateProvider.Startup))]

namespace ClimateProvider
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // To hotswap implementations, simply change this line.
            // Alternatively, we could throw this in the config.


           // var noaaServiceType = typeof(Services.NOAADummyService);
           // builder.Services.AddSingleton(typeof(Services.INOAAService), noaaServiceType);


        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
        }
    }
}
