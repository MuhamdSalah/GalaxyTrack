using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SpaceTrack.Services
{
    public class TLECalculationServiceDScheduler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TLECalculationServiceDScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                // Schedule at minute 56 of the current or next hour
                var targetTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 57, 30, DateTimeKind.Utc);
                if (now.Minute >= 57)
                {
                    // If the current time is past 56 minutes, schedule for the next hour
                    targetTime = targetTime.AddHours(1);
                }

                var delay = targetTime - now;
                await Task.Delay(delay, stoppingToken); // Wait until the scheduled time

                try
                {
                    //11
                    using var scope = _serviceProvider.CreateScope();
                    var tleService = scope.ServiceProvider.GetRequiredService<TLECalculationServiceD16>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions16ForNextHour at {DateTime.UtcNow} UTC");

                    await tleService.CalculateAndUpdatePositions16ForNextHourAsync();
                    //12

                    var tle2Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceD17>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions17ForNextHour at {DateTime.UtcNow} UTC");

                    await tle2Service.CalculateAndUpdatePositions17ForNextHourAsync();

                    //13
                    var tle3Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceD18>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions18ForNextHour at {DateTime.UtcNow} UTC");

                    await tle3Service.CalculateAndUpdatePositions18ForNextHourAsync();

                    ////14
                    //var tle4Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceD19>();

                    //Console.WriteLine($"Executing CalculateAndUpdatePositions19ForNextHour at {DateTime.UtcNow} UTC");

                    //await tle4Service.CalculateAndUpdatePositions19ForNextHourAsync();

                    ////15
                    //var tle5Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceD20>();

                    //Console.WriteLine($"Executing CalculateAndUpdatePositions20ForNextHour at {DateTime.UtcNow} UTC");

                    //await tle5Service.CalculateAndUpdatePositions20ForNextHourAsync();



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in scheduled task: {ex.Message}");
                }
            }
        }
    }
}
