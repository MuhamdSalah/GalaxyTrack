//for Qart
//using SpaceTrack.Services;
//using Asp.Net_Core_API.Extension;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using SpaceTrack.DAL;
//using SpaceTrack.Services;
//using MongoDB.Driver;
//using System.Text;
//using MathNet.Numerics;
//using Quartz;
//using Quartz.Impl;
//using Quartz.Spi;

//var builder = WebApplication.CreateBuilder(args);

//// 🔹 MongoDB Configuration
//string mongoConnectionString = builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString");
//string mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");

//// 🔹 Register MongoDB Dependencies
//builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));
//builder.Services.AddSingleton<IMongoDatabase>(sp =>
//{
//    var client = sp.GetRequiredService<IMongoClient>();
//    return client.GetDatabase(mongoDatabaseName);
//});
//builder.Services.AddSingleton<MongoDbContext>(sp =>
//{
//    var database = sp.GetRequiredService<IMongoDatabase>();
//    return new MongoDbContext(mongoConnectionString, mongoDatabaseName);
//});

//// 🔹 Add Services to the Container
//builder.Services.AddControllers();

//// 🔹 Configure JWT Authentication
//builder.Services.AddCustomJwtAuth(builder.Configuration);
//builder.Services.AddAuthorization();

//// 🔹 Register Application Services
//builder.Services.AddSingleton<SpaceTrack.Services.SpaceTrackTLE>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<ITLEService, TLEService>();

//// 🔹 Register TLE Calculation Services
//builder.Services.AddScoped<TLECalculationService>();
//builder.Services.AddScoped<TLECalculationService2>();
//builder.Services.AddScoped<TLECalculationService3>();
//builder.Services.AddScoped<TLECalculationService4>();
//builder.Services.AddScoped<TLECalculationService5>();
//builder.Services.AddScoped<TLECalculationServiceB6>();
//builder.Services.AddScoped<TLECalculationServiceB7>();
//builder.Services.AddScoped<TLECalculationServiceB8>();
//builder.Services.AddScoped<TLECalculationServiceB9>();
//builder.Services.AddScoped<TLECalculationServiceB10>();
//builder.Services.AddScoped<TLECalculationServiceC11>();
//builder.Services.AddScoped<TLECalculationServiceC12>();
//builder.Services.AddScoped<TLECalculationServiceC13>();
//builder.Services.AddScoped<TLECalculationServiceC14>();
//builder.Services.AddScoped<TLECalculationServiceC15>();
//builder.Services.AddScoped<TLECalculationServiceD16>();
//builder.Services.AddScoped<TLECalculationServiceD17>();
//builder.Services.AddScoped<TLECalculationServiceD18>();
//builder.Services.AddScoped<TLECalculationServiceD19>();
//builder.Services.AddScoped<TLECalculationServiceD20>();

//// 🔹 Register Additional Services
//builder.Services.AddScoped<PositionsService1>();

//// 🔹 Add Quartz (Job Scheduler)
//builder.Services.AddSingleton<IJobFactory, JobFactory>();
//builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
//builder.Services.AddSingleton<TLECalculationServiceScheduler>(); // Custom Scheduler
//builder.Services.AddSingleton<IJob, TLECalculationJob>();

//// 🔹 Start Scheduler as a Background Service
//builder.Services.AddHostedService<TLECalculationServiceScheduler>();

//// 🔹 Add Background Services for TLE Schedulers
//builder.Services.AddHostedService<TLEPayloadsScheduler>();
//builder.Services.AddHostedService<TLEdebrisScheduler>();
//builder.Services.AddHostedService<TLERocketScheduler>();
//builder.Services.AddHostedService<TLEUnknownScheduler>();

//// 🔹 Add Swagger/OpenAPI Support
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// 🔹 Configure HTTP Pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();
//for Qart

////////////////////////////////////////////////////////////////////////////////////////////WORK TILLSCUDLE OF EACH56

using Asp.Net_Core_API.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SpaceTrack.DAL;
using SpaceTrack.Services;
using MongoDB.Driver;
using System.Text;
using MathNet.Numerics;
using Quartz.Spi;


var builder = WebApplication.CreateBuilder(args);

// MongoDb configuration
string mongoConnectionString = builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString");
string mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");

// Register IMongoClient in DI Container
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));

// Register IMongoDatabase to be injected wherever needed
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoDatabaseName);
});

// Register MongoDbContext using the injected IMongoDatabase
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    return new MongoDbContext(mongoConnectionString, mongoDatabaseName);
});

// Add services to the container
builder.Services.AddControllers();

// Configure JWT authentication
builder.Services.AddCustomJwtAuth(builder.Configuration);
builder.Services.AddAuthorization();

// Register other services
builder.Services.AddSingleton<SpaceTrack.Services.SpaceTrackTLE>(); // Add your custom services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITLEService, TLEService>();
builder.Services.AddScoped<TLEService>();



// Add background services for timers
builder.Services.AddHostedService<TLEPayloadsScheduler>();
builder.Services.AddHostedService<TLEdebrisScheduler>();
builder.Services.AddHostedService<TLERocketScheduler>();
builder.Services.AddHostedService<TLEUnknownScheduler>();
builder.Services.AddHostedService<TLECalculationServiceScheduler>(); 
builder.Services.AddHostedService<TLECalculationServiceBScheduler>(); 
builder.Services.AddHostedService<TLECalculationServiceCScheduler>();
builder.Services.AddHostedService<TLECalculationServiceDScheduler>();

// Register SGP4Service with correct dependencies
builder.Services.AddScoped<TLECalculationService>();
builder.Services.AddScoped<TLECalculationService2>();
builder.Services.AddScoped<TLECalculationService3>();
builder.Services.AddScoped<TLECalculationService4>();
builder.Services.AddScoped<TLECalculationService5>();

builder.Services.AddScoped<TLECalculationServiceB6>();
builder.Services.AddScoped<TLECalculationServiceB7>();
builder.Services.AddScoped<TLECalculationServiceB8>();
builder.Services.AddScoped<TLECalculationServiceB9>();
builder.Services.AddScoped<TLECalculationServiceB10>();
builder.Services.AddScoped<TLECalculationServiceC11>();
builder.Services.AddScoped<TLECalculationServiceC12>();
builder.Services.AddScoped<TLECalculationServiceC13>();
builder.Services.AddScoped<TLECalculationServiceC14>();
builder.Services.AddScoped<TLECalculationServiceC15>();
builder.Services.AddScoped<TLECalculationServiceD16>();
builder.Services.AddScoped<TLECalculationServiceD17>();
builder.Services.AddScoped<TLECalculationServiceD18>();
builder.Services.AddScoped<TLECalculationServiceD19>();
builder.Services.AddScoped<TLECalculationServiceD20>();
builder.Services.AddScoped<PositionsService1>();
builder.Services.AddScoped<PositionsService2>(); 
builder.Services.AddScoped<PositionsService3>(); 
builder.Services.AddScoped<PositionsService4>();




// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

////////////////////////////////////////////////////////////////////////////////////////////WORK TILLSCUDLE OF EACH56
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////work till part 1 and half of part 2
//using Asp.Net_Core_API.Extension;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using SpaceTrack.DAL;
//using SpaceTrack.Services;
//using MongoDB.Driver;
//using System.Text;
//using MathNet.Numerics;

//var builder = WebApplication.CreateBuilder(args);

//// MongoDb configuration
//string mongoConnectionString = builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString");
//string mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");

//// Register MongoDbContext with IMongoDatabase dependency
//builder.Services.AddSingleton<IMongoDatabase>(sp =>
//{
//    var client = new MongoClient(mongoConnectionString);
//    return client.GetDatabase(mongoDatabaseName);
//});

//// Register MongoDbContext directly using retrieved values
//builder.Services.AddSingleton<MongoDbContext>(sp =>
//{
//    return new MongoDbContext(mongoConnectionString, mongoDatabaseName);
//});

//// Add services to the container
//builder.Services.AddControllers();

//// Configure JWT authentication
//builder.Services.AddCustomJwtAuth(builder.Configuration);
//builder.Services.AddAuthorization();

//// Register other services
//builder.Services.AddSingleton<SpaceTrack.Services.SpaceTrackTLE>(); // Add your custom services

//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<ITLEService, TLEService>();
////builder.Services.AddScoped<ObjectNameUpdateService>();
//builder.Services.AddSingleton<PayloadService>(); // For PayloadService

//// Add background services for timers
//builder.Services.AddHostedService<TLEPayloadsScheduler>();
//builder.Services.AddHostedService<TLEdebrisScheduler>();
//builder.Services.AddHostedService<TLERocketScheduler>();
//builder.Services.AddHostedService<TLEUnknownScheduler>();
//builder.Services.AddScoped<SGP4Service>();

//// Register MongoDbContext and inject ConnectionString and DatabaseName
//builder.Services.AddSingleton<MongoDbContext>(sp =>
//{
//    var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");
//    var connectionString = mongoSettings["ConnectionString"];
//    var databaseName = mongoSettings["DatabaseName"];
//    return new MongoDbContext(connectionString, databaseName);
//});

//builder.Services.AddControllers();

//// Add Swagger/OpenAPI support
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////for name and id

//using Asp.Net_Core_API.Extension;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using SpaceTrack.DAL;
//using SpaceTrack.Services;
//using System.Text;
//var builder = WebApplication.CreateBuilder(args);
//string mongoConnectionString = builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString");
//string mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");
//// Register MongoDbContext directly using retrieved values
//builder.Services.AddSingleton<MongoDbContext>(sp =>
//{
//    return new MongoDbContext(mongoConnectionString, mongoDatabaseName);
//});
//// Add services to the container.
/////////////////
//builder.Services.AddControllers();
//// Configure JWT authentication
//builder.Services.AddCustomJwtAuth(builder.Configuration);
//builder.Services.AddAuthorization();////////////////////////////
//// Add services to the container.
//builder.Services.AddSingleton<MongoDbContext>(sp =>
//    new MongoDbContext("mongodb://localhost:27017", "SpaceTrack"));
//builder.Services.AddSingleton<SpaceTrack.Services.SpaceTrackTLE>();/////////////////////////////////////new

//builder.Services.AddScoped<IUserService, UserService>();
//// Add services to the container.
//// Add services to the container.
////builder.Services.AddSingleton<MongoDbContext>();
//builder.Services.AddScoped<ITLEService, TLEService>();
//builder.Services.AddScoped<ObjectNameUpdateService>();/////////////////////////////////////////////////////////////////////////////////////ObjectName
//builder.Services.AddSingleton<PayloadService>();/******************************For download name and noardid**********************/

//builder.Services.AddScoped<TLEService>();//////////////////////////////////////////////For Timer

//builder.Services.AddHostedService<TLEPayloadsScheduler>(); // Register the background scheduler//////////////////////////////////////////////For Timer
//builder.Services.AddHostedService<TLEdebrisScheduler>();
//builder.Services.AddHostedService<TLERocketScheduler>();
//builder.Services.AddHostedService<TLEUnknownScheduler>();
//// Add services to the container.

//// Register MongoDbContext and inject ConnectionString and DatabaseName
//builder.Services.AddSingleton<MongoDbContext>(sp =>
//{
//    var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");
//    var connectionString = mongoSettings["ConnectionString"];
//    var databaseName = mongoSettings["DatabaseName"];
//    return new MongoDbContext(connectionString, databaseName);
//});


//builder.Services.AddControllers();

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
////builder.Services.AddCustomJwtAuth(builder.Configuration);
////builder.Services.AddAuthorization();
//var app = builder.Build();
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();
//*/
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////for name and id


//// Add services to the container.
//builder.Services.AddSingleton<MongoDbContext>();
//builder.Services.AddScoped<TLEService>();/////////////////////////









