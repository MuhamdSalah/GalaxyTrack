using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SpaceTrack.Services
{
    public class TLECalculationServiceCScheduler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TLECalculationServiceCScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                // Schedule at minute 56 of the current or next hour
                var targetTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 57, 0, DateTimeKind.Utc);
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
                    var tleService = scope.ServiceProvider.GetRequiredService<TLECalculationServiceC11>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions11ForNextHour at {DateTime.UtcNow} UTC");

                    await tleService.CalculateAndUpdatePositions11ForNextHourAsync();
                    //12

                    var tle2Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceC12>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions12ForNextHour at {DateTime.UtcNow} UTC");

                    await tle2Service.CalculateAndUpdatePositions12ForNextHourAsync();

                    //13
                    var tle3Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceC13>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions13ForNextHour at {DateTime.UtcNow} UTC");

                    await tle3Service.CalculateAndUpdatePositions13ForNextHourAsync();

                    //14
                    var tle4Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceC14>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions14ForNextHour at {DateTime.UtcNow} UTC");

                    await tle4Service.CalculateAndUpdatePositions14ForNextHourAsync();

                    //15
                    var tle5Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceC15>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions15ForNextHour at {DateTime.UtcNow} UTC");

                    await tle5Service.CalculateAndUpdatePositions15ForNextHourAsync();



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in scheduled task: {ex.Message}");
                }
            }
        }
    }
}
