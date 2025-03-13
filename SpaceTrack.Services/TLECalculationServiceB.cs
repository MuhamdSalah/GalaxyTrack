using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using SpaceTrack.DAL.Model;
using SGPdotNET.Propagation;
using SGPdotNET.TLE;

namespace SpaceTrack.Services
{
    public class TLECalculationServiceB6
    {
        private readonly IMongoCollection<SpaceObjectPosition6> _satelliteCollection6;
        private readonly IMongoCollection<TLEPayloads> _tleCollection6;

        public TLECalculationServiceB6(IMongoDatabase database)
        {
            _satelliteCollection6 = database.GetCollection<SpaceObjectPosition6>("Positions6");
            _tleCollection6 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions6ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the entire collection before inserting new data**
            await _satelliteCollection6.DeleteManyAsync(FilterDefinition<SpaceObjectPosition6>.Empty);

            // Fetch the next 100 TLEs (skip the first 500, take the next 100)
            var tleList = await _tleCollection6.Find(_ => true)
                                               .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                               .Skip(6500)
                                               .Limit(1300)
                                               .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition6>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions6 = new List<PositionRecord6>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions6.Add(new PositionRecord6
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition6
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions6 = positions6
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
                await _satelliteCollection6.InsertManyAsync(newRecords);
            }
        }
    }

    /* public class TLECalculationServiceB6
     {
         private readonly IMongoCollection<SpaceObjectPosition6> _satelliteCollection6;
         private readonly IMongoCollection<TLEPayloads> _tleCollection6;

         public TLECalculationServiceB6(IMongoDatabase database)
         {
             _satelliteCollection6 = database.GetCollection<SpaceObjectPosition6>("Positions6");
             _tleCollection6 = database.GetCollection<TLEPayloads>("TLEPayloads");
         }

         public async Task CalculateAndUpdatePositions6ForNextHour()
         {
             DateTime startTime = DateTime.UtcNow;
             DateTime endTime = startTime.AddHours(1);

             // Fetch the next 100 TLEs (skip the first 100, take the next 100)
             var tleList = _tleCollection6.Find(_ => true)
                                     .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                     .Skip(500) // Skip the first 100 TLEs
                                     .Limit(100) // Take the next 100 TLEs
                                     .ToList();

             if (!tleList.Any()) return;

             foreach (var tleData in tleList)
             {
                 try
                 {
                     var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                     var sgp4 = new Sgp4(tle);

                     var positions6 = new List<PositionRecord6>();

                     // Generate position data for each minute in the next hour
                     for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                     {
                         var eci = sgp4.FindPosition(timestamp);
                         var geo = eci.ToGeodetic();

                         positions6.Add(new PositionRecord6
                         {
                             Timestamp = timestamp,
                             Latitude = geo.Latitude.Degrees,
                             Longitude = geo.Longitude.Degrees,
                             Altitude = geo.Altitude
                         });
                     }

                     // Upsert logic: Insert if not exists, update otherwise
                     var filter = Builders<SpaceObjectPosition6>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                     var update = Builders<SpaceObjectPosition6>.Update
                         .SetOnInsert(x => x.NoradId, tleData.NoradId)
                         .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                         .PushEach(x => x.Positions6, positions6);

                     _satelliteCollection6.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                 }
             }
         }
     }*/
    //7
    public class TLECalculationServiceB7
    {
        private readonly IMongoCollection<SpaceObjectPosition7> _satelliteCollection7;
        private readonly IMongoCollection<TLEPayloads> _tleCollection7;

        public TLECalculationServiceB7(IMongoDatabase database)
        {
            _satelliteCollection7 = database.GetCollection<SpaceObjectPosition7>("Positions7");
            _tleCollection7 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions7ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the collection before inserting new data**
            await _satelliteCollection7.DeleteManyAsync(FilterDefinition<SpaceObjectPosition7>.Empty);

            // Fetch the next 100 TLEs (skip the first 600, take the next 100)
            var tleList = await _tleCollection7.Find(_ => true)
                                               .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                               .Skip(7800)
                                               .Limit(1300)
                                               .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition7>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions7 = new List<PositionRecord7>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions7.Add(new PositionRecord7
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition7
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions7 = positions7
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
                await _satelliteCollection7.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceB7
    {
        private readonly IMongoCollection<SpaceObjectPosition7> _satelliteCollection7;
        private readonly IMongoCollection<TLEPayloads> _tleCollection7;

        public TLECalculationServiceB7(IMongoDatabase database)
        {
            _satelliteCollection7 = database.GetCollection<SpaceObjectPosition7>("Positions7");
            _tleCollection7 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions7ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);




            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = _tleCollection7.Find(_ => true)
                                    .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                    .Skip(600) // Skip the first 100 TLEs
                                    .Limit(100) // Take the next 100 TLEs
                                    .ToList();

            if (!tleList.Any()) return;

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions7 = new List<PositionRecord7>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions7.Add(new PositionRecord7
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition7>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition7>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                            .PushEach(x => x.Positions7, positions7);

                    //.PushEach(" SpaceObjectPosition7.Positions7", positions7);
                    //.PushEach("Positions2", positions2);
                    //.PushEach(x => x.Positions2, positions2.ToArray());

                    //

                    _satelliteCollection7.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }

    }*/
    //8
    public class TLECalculationServiceB8
    {
        private readonly IMongoCollection<SpaceObjectPosition8> _satelliteCollection8;
        private readonly IMongoCollection<TLEPayloads> _tleCollection8;

        public TLECalculationServiceB8(IMongoDatabase database)
        {
            _satelliteCollection8 = database.GetCollection<SpaceObjectPosition8>("Positions8");
            _tleCollection8 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions8ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the collection before inserting new data**
            await _satelliteCollection8.DeleteManyAsync(FilterDefinition<SpaceObjectPosition8>.Empty);

            // Fetch the next 100 TLEs (skip the first 700, take the next 100)
            var tleList = await _tleCollection8.Find(_ => true)
                                               .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                               .Skip(9100)
                                               .Limit(1300)
                                               .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition8>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions8 = new List<PositionRecord8>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions8.Add(new PositionRecord8
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition8
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions8 = positions8
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
                await _satelliteCollection8.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceB8
    {
        private readonly IMongoCollection<SpaceObjectPosition8> _satelliteCollection8;
        private readonly IMongoCollection<TLEPayloads> _tleCollection8;

        public TLECalculationServiceB8(IMongoDatabase database)
        {
            _satelliteCollection8 = database.GetCollection<SpaceObjectPosition8>("Positions8");
            _tleCollection8 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions8ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);
            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = _tleCollection8.Find(_ => true)
                                    .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                    .Skip(700) // Skip the first 100 TLEs
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

                    var positions8 = new List<PositionRecord8>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions8.Add(new PositionRecord8
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition8>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition8>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(x => x.Positions8, positions8);

                    //.PushEach(" SpaceObjectPosition3.Positions8", positions8);
                    //.PushEach(x => x.Positions3, positions3);

                    _satelliteCollection8.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }
    }*/
    //9
    public class TLECalculationServiceB9
    {
        private readonly IMongoCollection<SpaceObjectPosition9> _satelliteCollection9;
        private readonly IMongoCollection<TLEPayloads> _tleCollection9;

        public TLECalculationServiceB9(IMongoDatabase database)
        {
            _satelliteCollection9 = database.GetCollection<SpaceObjectPosition9>("Positions9");
            _tleCollection9 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions9ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the collection before inserting new data**
            await _satelliteCollection9.DeleteManyAsync(FilterDefinition<SpaceObjectPosition9>.Empty);

            // Fetch the next 100 TLEs (skip the first 800, take the next 100)
            var tleList = await _tleCollection9.Find(_ => true)
                                               .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                               .Skip(10400)
                                               .Limit(1300)
                                               .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition9>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions9 = new List<PositionRecord9>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions9.Add(new PositionRecord9
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition9
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions9 = positions9
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
                await _satelliteCollection9.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceB9
    {
        private readonly IMongoCollection<SpaceObjectPosition9> _satelliteCollection9;
        private readonly IMongoCollection<TLEPayloads> _tleCollection9;

        public TLECalculationServiceB9(IMongoDatabase database)
        {
            _satelliteCollection9 = database.GetCollection<SpaceObjectPosition9>("Positions9");
            _tleCollection9 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions9ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);
            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = _tleCollection9.Find(_ => true)
                                    .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                    .Skip(800) // Skip the first 100 TLEs
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

                    var positions9 = new List<PositionRecord9>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions9.Add(new PositionRecord9
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition9>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition9>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(x => x.Positions9, positions9);

                        //.PushEach(" SpaceObjectPosition9.Positions9", positions9);
                    //.PushEach(x => x.Positions3, positions3);

                    _satelliteCollection9.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }
    }*/
    public class TLECalculationServiceB10
    {
        private readonly IMongoCollection<SpaceObjectPosition10> _satelliteCollection10;
        private readonly IMongoCollection<TLEPayloads> _tleCollection10;

        public TLECalculationServiceB10(IMongoDatabase database)
        {
            _satelliteCollection10 = database.GetCollection<SpaceObjectPosition10>("Positions10");
            _tleCollection10 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions10ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the collection before inserting new data**
            await _satelliteCollection10.DeleteManyAsync(FilterDefinition<SpaceObjectPosition10>.Empty);

            // Fetch the next 100 TLEs (skip the first 900, take the next 100)
            var tleList = await _tleCollection10.Find(_ => true)
                                                .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                                .Skip(11700)
                                                .Limit(1300)
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition10>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions10 = new List<PositionRecord10>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions10.Add(new PositionRecord10
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition10
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions10 = positions10
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
                await _satelliteCollection10.InsertManyAsync(newRecords);
            }
        }
    }

    /* public class TLECalculationServiceB10
     {
         private readonly IMongoCollection<SpaceObjectPosition10> _satelliteCollection10;
         private readonly IMongoCollection<TLEPayloads> _tleCollection10;

         public TLECalculationServiceB10(IMongoDatabase database)
         {
             _satelliteCollection10 = database.GetCollection<SpaceObjectPosition10>("Positions10");
             _tleCollection10 = database.GetCollection<TLEPayloads>("TLEPayloads");
         }

         public async Task CalculateAndUpdatePositions10ForNextHour()
         {
             DateTime startTime = DateTime.UtcNow;
             DateTime endTime = startTime.AddHours(1);

             var tleList = _tleCollection10.Find(_ => true)
                                      .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                      .Skip(900) // Skip the first 100 TLEs
                                      .Limit(100) // Take the next 100 TLEs
                                      .ToList();

             if (!tleList.Any()) return;

             foreach (var tleData in tleList)
             {
                 try
                 {
                     var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                     var sgp4 = new Sgp4(tle);

                     var positions10 = new List<PositionRecord10>();

                     // Generate position data for each minute in the next hour
                     for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                     {
                         var eci = sgp4.FindPosition(timestamp);
                         var geo = eci.ToGeodetic();

                         positions10.Add(new PositionRecord10
                         {
                             Timestamp = timestamp,
                             Latitude = geo.Latitude.Degrees,
                             Longitude = geo.Longitude.Degrees,
                             Altitude = geo.Altitude
                         });
                     }

                     // Upsert logic: Insert if not exists, update otherwise
                     var filter = Builders<SpaceObjectPosition10>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                     var update = Builders<SpaceObjectPosition10>.Update
                         .SetOnInsert(x => x.NoradId, tleData.NoradId)
                         .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                         .PushEach(x => x.Positions10, positions10);

                     _satelliteCollection10.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                 }
             }
         }
     }*/

}