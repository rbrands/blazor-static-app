using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.Shared;
using BlazorApp.Api.Repositories;

[assembly: FunctionsStartup(typeof(BlazorApp.Api.Startup))]
namespace BlazorApp.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFilter(level => true);
            });



            var config = new ConfigurationBuilder()
                           .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                           .AddEnvironmentVariables()
                           .Build();
            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton<BlobStorageRepository>();

        }
    }
}
