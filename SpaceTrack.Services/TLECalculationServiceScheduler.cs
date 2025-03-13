using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SpaceTrack.Services
{
    public class TLECalculationServiceScheduler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TLECalculationServiceScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                // Schedule at minute 56 of the current or next hour
                var targetTime = new DateTime(now.Year, now.Month, now.Day, now.Hour,56, 0, DateTimeKind.Utc);
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
                    var tleService = scope.ServiceProvider.GetRequiredService<TLECalculationService>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositionsForNextHour at {DateTime.UtcNow} UTC");

                    await tleService.CalculateAndUpdatePositionsForNextHour();

                    var tle2Service = scope.ServiceProvider.GetRequiredService<TLECalculationService2>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions2ForNextHour at {DateTime.UtcNow} UTC");

                    await tle2Service.CalculateAndUpdatePositions2ForNextHour();

                    //3
                    var tle3Service = scope.ServiceProvider.GetRequiredService<TLECalculationService3>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions3ForNextHour at {DateTime.UtcNow} UTC");

                    await tle3Service.CalculateAndUpdatePositions3ForNextHour();

                    //4
                    var tle4Service = scope.ServiceProvider.GetRequiredService<TLECalculationService4>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions4ForNextHour at {DateTime.UtcNow} UTC");

                    await tle4Service.CalculateAndUpdatePositions4ForNextHour();

                    //5
                    var tle5Service = scope.ServiceProvider.GetRequiredService<TLECalculationService5>();

                    Console.WriteLine($"Executing CalculateAndUpdatePositions5ForNextHour at {DateTime.UtcNow} UTC");

                    await tle5Service.CalculateAndUpdatePositions5ForNextHourAsync();



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in scheduled task: {ex.Message}");
                }
            }
        }
    }
}

////////////////////////one time
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace SpaceTrack.Services
//{
//    public class TLECalculationServiceScheduler : BackgroundService
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public TLECalculationServiceScheduler(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                var now = DateTime.UtcNow; // Use UTC for consistency
//                var targetTime = new DateTime(now.Year, now.Month, now.Day, 14, 23, 0, DateTimeKind.Utc); // 7:30 AM UTC

//                if (now > targetTime)
//                {
//                    targetTime = targetTime.AddDays(1); // Schedule for the next day if the time has passed
//                }

//                var delay = targetTime - now;
//                await Task.Delay(delay, stoppingToken); // Wait until the scheduled time

//                try
//                {
//                    using var scope = _serviceProvider.CreateScope();
//                    //var tleService = scope.ServiceProvider.GetRequiredService<TLECalculationServiceScheduler>();
//                    var tleService = scope.ServiceProvider.GetRequiredService<TLECalculationService>();
//                    Console.WriteLine($"Registered Services Count:");


//                    await tleService.CalculateAndUpdatePositionsForNextHour(); // Call the method

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
///////////////////////////////one time
//using Quartz;
//using Quartz.Impl;
//using Quartz.Spi;
//using System;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace SpaceTrack.Services
//{
//    public class TLECalculationServiceScheduler : IHostedService
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public TLECalculationServiceScheduler(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task StartSchedulerAsync()
//        {
//            // Create scheduler factory
//            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
//            IScheduler scheduler = await schedulerFactory.GetScheduler();

//            // Set custom job factory for Dependency Injection (DI)
//            scheduler.JobFactory = new JobFactory(_serviceProvider);

//            // Start the scheduler
//            await scheduler.Start();

//            // Define the job and tie it to TLECalculationJob
//            IJobDetail job = JobBuilder.Create<TLECalculationJob>()
//                .WithIdentity("TLECalculationJob", "group1")
//                .Build();

//            // Create a trigger that runs every hour at minute 56
//            ITrigger trigger = TriggerBuilder.Create()
//                .WithIdentity("TLECalculationTrigger", "group1")
//                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0, 56)
//                .InTimeZone(TimeZoneInfo.Utc)) // Ensure Quartz v3+ is installed
//                .Build();

//            // Schedule the job
//            await scheduler.ScheduleJob(job, trigger);
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    // Job Factory Implementation
//    public class JobFactory : IJobFactory
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public JobFactory(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
//        {
//            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
//        }

//        public void ReturnJob(IJob job)
//        {
//            (job as IDisposable)?.Dispose();
//        }
//    }

//    // Job Implementation
//    public class TLECalculationJob : IJob
//    {
//        private readonly TLECalculationService _tleCalculationService;

//        public TLECalculationJob(TLECalculationService tleCalculationService)
//        {
//            _tleCalculationService = tleCalculationService;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            await _tleCalculationService.PerformCalculationAsync();
//        }
//    }
//}
