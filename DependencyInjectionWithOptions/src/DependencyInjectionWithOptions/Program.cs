﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using static System.Console;

namespace DependencyInjectionWithOptions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //RegisterServicesWithOptions();

            RegisterServicesWithConfig();
            UseServices();
        }

        private static void RegisterServicesWithConfig()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot config = configBuilder.Build();

            var services = new ServiceCollection();
            services.AddTransient<HelloController>();
            services.AddGreetingService(config);

            Container = services.BuildServiceProvider();
        }

        private static void UseServices()
        {
            var controller = Container.GetService<HelloController>();

            string greeting = controller.Action("Stephanie");

            WriteLine(greeting);
        }

        private static void RegisterServicesWithOptions()
        {
            var services = new ServiceCollection();
            services.AddOptions();

            services.AddTransient<HelloController>();

            services.AddGreetingService(options =>
            {
                options.From = "Christian";
            });

            Container = services.BuildServiceProvider();
        }

        public static IServiceProvider Container { get; private set; }
    }
}