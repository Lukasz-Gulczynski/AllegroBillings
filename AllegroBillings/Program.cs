using AllegroBillingEntries.BussinesLogic.Commands;
using AllegroBillings.BusinessLogic.Services;
using AllegroBillings.Contracts.Configurations;
using AllegroBillings.Contracts.Interfaces.Repositories;
using AllegroBillings.Contracts.Interfaces.Services;
using AllegroBillings.Data;
using AllegroBillings.Data.Implementations;
using AllegroBillings.Scheduler.Jobs;
using AllegroBillings.Scheduler.Quartz;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz.Impl;
using Quartz.Spi;

namespace AllegroBillingEntries
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BillingContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while initializing the database.");
                }
            }

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<AllegroApiSettings>(context.Configuration.GetSection("AllegroApiSettings"));

                    services.AddDbContext<BillingContext>(options =>
                    {
                        options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection"));
                    });

                    services.AddScoped<IBillingRepository, BillingRepository>();
                    services.AddScoped<IBillingService, BillingService>();
                    services.AddScoped<OfferBillingServiceCommand>();
                    services.AddScoped<OrderBillingServiceCommand>();

                    services.AddSingleton<IJobFactory, QuartzJobFactory>();
                    services.AddSingleton(provider =>
                    {
                        var schedulerFactory = new StdSchedulerFactory();
                        var scheduler = schedulerFactory.GetScheduler().Result;
                        scheduler.JobFactory = provider.GetService<IJobFactory>();
                        return scheduler;
                    });

                    services.AddSingleton(new JobSchedule(typeof(OfferBillingJob), "0 0 4 * * ?")); // Codziennie o 4 rano
                    services.AddSingleton(new JobSchedule(typeof(OrderBillingJob), "0 0/5 * * * ?")); // Co 5 minut

                    services.AddHostedService<QuartzHostedService>();
                });
    }
}
