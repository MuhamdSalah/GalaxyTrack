using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using SpaceTrack.DAL.Model;
using SGPdotNET.Propagation;
using SGPdotNET.TLE;

namespace SpaceTrack.Services
{
    //11
    public class TLECalculationServiceC11
    {
        private readonly IMongoCollection<SpaceObjectPosition11> _debrisCollection11;
        private readonly IMongoCollection<TLEDebris> _tleCollection11;

        public TLECalculationServiceC11(IMongoDatabase database)
        {
            _debrisCollection11 = database.GetCollection<SpaceObjectPosition11>("Positions11");
            _tleCollection11 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public async Task CalculateAndUpdatePositions11ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the collection before inserting new data**
            await _debrisCollection11.DeleteManyAsync(FilterDefinition<SpaceObjectPosition11>.Empty);

            // Fetch the first 100 TLEs, ordered by Timestamp
            var tleList = await _tleCollection11.Find(_ => true)
                                                .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                                .Limit(1300)
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition11>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions11 = new List<PositionRecord11>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions11.Add(new PositionRecord11
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition11
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions11 = positions11
                    };

                    newRecords.Add(newRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }

            // Insert all new records in bulk
            if (newRecords.Any())
            {
                await _debrisCollection11.InsertManyAsync(newRecords);
            }
        }
    }

    /* public class TLECalculationServiceC11
     {
         private readonly IMongoCollection<SpaceObjectPosition11> _DebrisCollection11;
         private readonly IMongoCollection<TLEDebris> _tleCollection11;

         public TLECalculationServiceC11(IMongoDatabase database)
         {
             _DebrisCollection11 = database.GetCollection<SpaceObjectPosition11>("Positions11");
             _tleCollection11 = database.GetCollection<TLEDebris>("TLEDebris");
         }

         public void CalculateAndUpdatePositions11ForNextHour()
         {
             DateTime startTime = DateTime.UtcNow;
             DateTime endTime = startTime.AddHours(1);

             // Fetch the first 100 TLEs, ordered by Timestamp
             var tleList = _tleCollection11.Find(_ => true)
                                         .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                         .Limit(100)
                                         .ToList();

             if (!tleList.Any()) return;

             foreach (var tleData in tleList)
             {
                 try
                 {
                     var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                     var sgp4 = new Sgp4(tle);

                     var positions11 = new List<PositionRecord11>();

                     // Generate position data for each minute in the next hour
                     for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                     {
                         var eci = sgp4.FindPosition(timestamp);
                         var geo = eci.ToGeodetic();

                         positions11.Add(new PositionRecord11
                         {
                             Timestamp = timestamp,
                             Latitude = geo.Latitude.Degrees,
                             Longitude = geo.Longitude.Degrees,
                             Altitude = geo.Altitude
                         });
                     }

                     // Upsert logic: Insert if not exists, update otherwise
                     var filter = Builders<SpaceObjectPosition11>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                     var update = Builders<SpaceObjectPosition11>.Update
                         .SetOnInsert(x => x.NoradId, tleData.NoradId)
                         .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                         .PushEach(x => x.Positions11, positions11);

                     _DebrisCollection11.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                 }
             }
         }
     }*/
    //12
    public class TLECalculationServiceC12
    {
        private readonly IMongoCollection<SpaceObjectPosition12> _debrisCollection12;
        private readonly IMongoCollection<TLEDebris> _tleCollection12;

        public TLECalculationServiceC12(IMongoDatabase database)
        {
            _debrisCollection12 = database.GetCollection<SpaceObjectPosition12>("Positions12");
            _tleCollection12 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public async Task CalculateAndUpdatePositions12ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the collection before inserting new data**
            await _debrisCollection12.DeleteManyAsync(FilterDefinition<SpaceObjectPosition12>.Empty);

            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = await _tleCollection12.Find(_ => true)
                                                .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                                .Skip(2600) // Skip the first 100 TLEs
                                                .Limit(1300) // Take the next 100 TLEs
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition12>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions12 = new List<PositionRecord12>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions12.Add(new PositionRecord12
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition12
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions12 = positions12
                    };

                    newRecords.Add(newRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }

            // Insert all new records in bulk
            if (newRecords.Any())
            {
                await _debrisCollection12.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceC12
    {
        private readonly IMongoCollection<SpaceObjectPosition12> _DebrisCollection12;
        private readonly IMongoCollection<TLEDebris> _tleCollection12;

        public TLECalculationServiceC12(IMongoDatabase database)
        {
            _DebrisCollection12 = database.GetCollection<SpaceObjectPosition12>("Positions12");
            _tleCollection12 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public void CalculateAndUpdatePositions12ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);




            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = _tleCollection12.Find(_ => true)
                                    .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                    .Skip(100) // Skip the first 100 TLEs
                                    .Limit(100) // Take the next 100 TLEs
                                    .ToList();

            if (!tleList.Any()) return;

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions12 = new List<PositionRecord12>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions12.Add(new PositionRecord12
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition12>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition12>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(" SpaceObjectPosition12.Positions12", positions12);
                    //.PushEach("Positions2", positions2);
                    //.PushEach(x => x.Positions2, positions2.ToArray());

                    //

                    _DebrisCollection12.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }

    }*/
    //13
    public class TLECalculationServiceC13
    {
        private readonly IMongoCollection<SpaceObjectPosition13> _debrisCollection13;
        private readonly IMongoCollection<TLEDebris> _tleCollection13;

        public TLECalculationServiceC13(IMongoDatabase database)
        {
            _debrisCollection13 = database.GetCollection<SpaceObjectPosition13>("Positions13");
            _tleCollection13 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public async Task CalculateAndUpdatePositions13ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear old data before inserting new positions**
            await _debrisCollection13.DeleteManyAsync(FilterDefinition<SpaceObjectPosition13>.Empty);

            // Fetch the next 100 TLEs (skip the first 200, take the next 100)
            var tleList = await _tleCollection13.Find(_ => true)
                                                .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                                .Skip(3900) // Skip the first 200 TLEs
                                                .Limit(1300) // Take the next 100 TLEs
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition13>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions13 = new List<PositionRecord13>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions13.Add(new PositionRecord13
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition13
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions13 = positions13
                    };

                    newRecords.Add(newRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }

            // Insert all new records in bulk
            if (newRecords.Any())
            {
                await _debrisCollection13.InsertManyAsync(newRecords);
            }
        }
    }

    /*  public class TLECalculationServiceC13
      {
          private readonly IMongoCollection<SpaceObjectPosition13> _DebrisCollection13;
          private readonly IMongoCollection<TLEDebris> _tleCollection13;

          public TLECalculationServiceC13(IMongoDatabase database)
          {
              _DebrisCollection13 = database.GetCollection<SpaceObjectPosition13>("Positions13");
              _tleCollection13 = database.GetCollection<TLEDebris>("TLEDebris");
          }

          public void CalculateAndUpdatePositions13ForNextHour()
          {
              DateTime startTime = DateTime.UtcNow;
              DateTime endTime = startTime.AddHours(1);
              // Fetch the next 100 TLEs (skip the first 100, take the next 100)
              var tleList = _tleCollection13.Find(_ => true)
                                      .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                      .Skip(200) // Skip the first 100 TLEs
                                      .Limit(100) // Take the next 100 TLEs
                                      .ToList();

              // Fetch the first 100 TLEs, ordered by Timestamp
              //var tleList = _tleCollection.Find(_ => true)
              //                            .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
              //                            .Limit(100)
              //                            .ToList();

              if (!tleList.Any()) return;

              foreach (var tleData in tleList)
              {
                  try
                  {
                      var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                      var sgp4 = new Sgp4(tle);

                      var positions13 = new List<PositionRecord13>();

                      // Generate position data for each minute in the next hour
                      for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                      {
                          var eci = sgp4.FindPosition(timestamp);
                          var geo = eci.ToGeodetic();

                          positions13.Add(new PositionRecord13
                          {
                              Timestamp = timestamp,
                              Latitude = geo.Latitude.Degrees,
                              Longitude = geo.Longitude.Degrees,
                              Altitude = geo.Altitude
                          });
                      }

                      // Upsert logic: Insert if not exists, update otherwise
                      var filter = Builders<SpaceObjectPosition13>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                      var update = Builders<SpaceObjectPosition13>.Update
                          .SetOnInsert(x => x.NoradId, tleData.NoradId)
                          .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                          .PushEach(" SpaceObjectPosition13.Positions13", positions13);
                      //.PushEach(x => x.Positions3, positions3);

                      _DebrisCollection13.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                  }
              }
          }
      }*/
    //14
    public class TLECalculationServiceC14
    {
        private readonly IMongoCollection<SpaceObjectPosition14> _debrisCollection14;
        private readonly IMongoCollection<TLEDebris> _tleCollection14;

        public TLECalculationServiceC14(IMongoDatabase database)
        {
            _debrisCollection14 = database.GetCollection<SpaceObjectPosition14>("Positions14");
            _tleCollection14 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public async Task CalculateAndUpdatePositions14ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear old data before inserting new positions**
            await _debrisCollection14.DeleteManyAsync(FilterDefinition<SpaceObjectPosition14>.Empty);

            // Fetch the next 100 TLEs (skip the first 300, take the next 100)
            var tleList = await _tleCollection14.Find(_ => true)
                                                .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                                .Skip(5200) // Skip the first 300 TLEs
                                                .Limit(1300) // Take the next 100 TLEs
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition14>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions14 = new List<PositionRecord14>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions14.Add(new PositionRecord14
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition14
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions14 = positions14
                    };

                    newRecords.Add(newRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }

            // Insert all new records in bulk
            if (newRecords.Any())
            {
                await _debrisCollection14.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceC14
    {
        private readonly IMongoCollection<SpaceObjectPosition14> _DebrisCollection14;
        private readonly IMongoCollection<TLEDebris> _tleCollection14;

        public TLECalculationServiceC14(IMongoDatabase database)
        {
            _DebrisCollection14 = database.GetCollection<SpaceObjectPosition14>("Positions14");
            _tleCollection14 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public void CalculateAndUpdatePositions14ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);
            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = _tleCollection14.Find(_ => true)
                                    .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                    .Skip(300) // Skip the first 100 TLEs
                                    .Limit(100) // Take the next 100 TLEs
                                    .ToList();

            // Fetch the first 100 TLEs, ordered by Timestamp
            //var tleList = _tleCollection.Find(_ => true)
            //                            .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
            //                            .Limit(100)
            //                            .ToList();

            if (!tleList.Any()) return;

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions14 = new List<PositionRecord14>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions14.Add(new PositionRecord14
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition14>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition14>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(" SpaceObjectPosition14.Positions14", positions14);
                    //.PushEach(x => x.Positions3, positions3);

                    _DebrisCollection14.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }
    }*/
    public class TLECalculationServiceC15
    {
        private readonly IMongoCollection<SpaceObjectPosition15> _debrisCollection15;
        private readonly IMongoCollection<TLEDebris> _tleCollection15;

        public TLECalculationServiceC15(IMongoDatabase database)
        {
            _debrisCollection15 = database.GetCollection<SpaceObjectPosition15>("Positions15");
            _tleCollection15 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public async Task CalculateAndUpdatePositions15ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear old data before inserting new positions**
            await _debrisCollection15.DeleteManyAsync(FilterDefinition<SpaceObjectPosition15>.Empty);

            // Fetch the next 100 TLEs (skip the first 400, take the next 100)
            var tleList = await _tleCollection15.Find(_ => true)
                                                .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                                .Skip(6500) // Skip the first 400 TLEs
                                                .Limit(1300) // Take the next 100 TLEs
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition15>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions15 = new List<PositionRecord15>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions15.Add(new PositionRecord15
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition15
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions15 = positions15
                    };

                    newRecords.Add(newRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }

            // Insert all new records in bulk
            if (newRecords.Any())
            {
                await _debrisCollection15.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceC15
    {
        private readonly IMongoCollection<SpaceObjectPosition15> _DebrisCollection15;
        private readonly IMongoCollection<TLEDebris> _tleCollection15;

        public TLECalculationServiceC15(IMongoDatabase database)
        {
            _DebrisCollection15 = database.GetCollection<SpaceObjectPosition15>("Positions15");
            _tleCollection15 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public void CalculateAndUpdatePositions15ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            var tleList = _tleCollection15.Find(_ => true)
                                     .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                     .Skip(400) // Skip the first 100 TLEs
                                     .Limit(100) // Take the next 100 TLEs
                                     .ToList();

            if (!tleList.Any()) return;

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions15 = new List<PositionRecord15>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions15.Add(new PositionRecord15
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition15>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition15>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(x => x.Positions15, positions15);

                    _DebrisCollection15.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }
    }*/

}