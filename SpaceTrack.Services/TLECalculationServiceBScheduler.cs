using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SpaceTrack.Services
{
    public class TLECalculationServiceBScheduler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TLECalculationServiceBScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                // Schedule at minute 56 of the current or next hour
                var targetTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 56, 30, DateTimeKind.Utc);
                if (now.Minute >= 56)
                {
                    // If the current time is past 56 minutes, schedule for the next hour
                    targetTime = targetTime.AddHours(1);
                }

                var delay = targetTime - now;
                await Task.Delay(delay, stoppingToken); // Wait until the scheduled time

                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var tleService = scope.ServiceProvider.GetRequiredService<TLECalculationServiceB6>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions6ForNextHour at {DateTime.UtcNow} UTC");

                    await tleService.CalculateAndUpdatePositions6ForNextHour();

                    var tle2Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceB7>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions7ForNextHour at {DateTime.UtcNow} UTC");

                    await tle2Service.CalculateAndUpdatePositions7ForNextHour();

                    //3
                    var tle3Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceB8>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions8ForNextHour at {DateTime.UtcNow} UTC");

                    await tle3Service.CalculateAndUpdatePositions8ForNextHour();

                    //4
                    var tle4Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceB9>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions9ForNextHour at {DateTime.UtcNow} UTC");

                    await tle4Service.CalculateAndUpdatePositions9ForNextHour();

                    //5
                    var tle5Service = scope.ServiceProvider.GetRequiredService<TLECalculationServiceB10>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions10ForNextHour at {DateTime.UtcNow} UTC");

                    await tle5Service.CalculateAndUpdatePositions10ForNextHour();



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in scheduled task: {ex.Message}");
                }
            }
        }
    }
}
