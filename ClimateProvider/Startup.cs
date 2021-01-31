using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ClimateProvider.Startup))]

namespace ClimateProvider
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
           // builder.Services.AddSingleton<Services.INOAAService>(new WhateverHeNamesTheClass())
        }
    }
}
