﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Georest.Api.Models;
using Georest.Domain.Models;
using Serilog;

namespace Georest.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true, true)
            .AddEnvironmentVariables()
            .Build();
        public static void Main(string[] args)
        {
            CurrentDirectoryHelpers.SetCurrentDirectory();

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.WithProperty("App Name", "Georest.Api")
                .CreateLogger();
            try
            {
                IWebHost host = CreateWebHostBuilder(args).Build();

                using (IServiceScope serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                    {
                        context.Database.Migrate();
                    }
                }

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
