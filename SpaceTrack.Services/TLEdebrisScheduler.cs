using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SpaceTrack.Services
{
    public class TLEdebrisScheduler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        // Define the scheduled times
        private static readonly TimeSpan[] ScheduledTimes =
        {
             new TimeSpan(8, 3, 0),   // 08:00 UTC
            new TimeSpan(16, 3, 0),  // 16:00 UTC
            new TimeSpan(23, 56, 0)  // 23:59 UTC
        };

        public TLEdebrisScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                // Find the next closest execution time
                var nextRun = ScheduledTimes
                    .Select(time => new DateTime(now.Year, now.Month, now.Day, time.Hours, time.Minutes, time.Seconds, DateTimeKind.Utc))
                    .Where(dt => dt > now)
                    .OrderBy(dt => dt)
                    .FirstOrDefault();

                // If no times remain for today, schedule for the first time tomorrow
                if (nextRun == default)
                {
                    nextRun = new DateTime(now.Year, now.Month, now.Day, ScheduledTimes[0].Hours, ScheduledTimes[0].Minutes, ScheduledTimes[0].Seconds, DateTimeKind.Utc)
                        .AddDays(1);
                }

                var delay = nextRun - now;
                Console.WriteLine($"Next debris TLE update scheduled at: {nextRun:yyyy-MM-dd HH:mm:ss} UTC");

                try
                {
                    await Task.Delay(delay, stoppingToken); // Wait until the scheduled time

                    using var scope = _serviceProvider.CreateScope();
                    var tleService = scope.ServiceProvider.GetRequiredService<TLEService>();

                    Console.WriteLine($"Starting debris TLE update at: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");

                    await tleService.SaveOrUpdateAllDebrisTLEsAsync(); // Execute the method

                    Console.WriteLine($"Debris TLE update completed at: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
                }
                catch (TaskCanceledException)
                {
                    // Gracefully handle cancellation
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in scheduled debris TLE update: {ex.Message}");
                }
            }
        }
    }
}
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using MathNet.Numerics;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace SpaceTrack.Services
//{
//    public class TLEdebrisScheduler : BackgroundService
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public TLEdebrisScheduler(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                var now = DateTime.UtcNow; // Use UTC for consistency
//                var targetTime = new DateTime(now.Year, now.Month, now.Day, 2, 24, 0, DateTimeKind.Utc); // 7:30 AM UTC

//                if (now > targetTime)
//                {
//                    targetTime = targetTime.AddDays(1); // Schedule for the next day if the time has passed
//                }

//                var delay = targetTime - now;
//                await Task.Delay(delay, stoppingToken); // Wait until the scheduled time

//                try
//                {
//                    using var scope = _serviceProvider.CreateScope();
//                    var tleService = scope.ServiceProvider.GetRequiredService<TLEService>();
//                    Console.WriteLine($"Registered Services Count:");


//                    await tleService.SaveOrUpdateAllDebrisTLEsAsync(); // Call the method
//                }
//                catch (Exception ex)
//                {
//                    // Log errors (optional: integrate a logging framework)
//                    Console.WriteLine($"Error in scheduled task: {ex.Message}");
//                }
//            }
//        }
//    }
//}



