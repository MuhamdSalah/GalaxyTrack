using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using SpaceTrack.DAL.Model;
using SGPdotNET.Propagation;
using SGPdotNET.TLE;

namespace SpaceTrack.Services
{
    //16
    public class TLECalculationServiceD16
    {
        private readonly IMongoCollection<SpaceObjectPosition16> _debrisCollection16;
        private readonly IMongoCollection<TLEDebris> _tleCollection16;

        public TLECalculationServiceD16(IMongoDatabase database)
        {
            _debrisCollection16 = database.GetCollection<SpaceObjectPosition16>("Positions16");
            _tleCollection16 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public async Task CalculateAndUpdatePositions16ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear old data before inserting new positions**
            await _debrisCollection16.DeleteManyAsync(FilterDefinition<SpaceObjectPosition16>.Empty);

            // Fetch the next 100 TLEs (skip the first 500, take the next 100)
            var tleList = await _tleCollection16.Find(_ => true)
                                                .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                                .Skip(7800) // Skip the first 500 TLEs
                                                .Limit(1300) // Take the next 100 TLEs
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition16>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions16 = new List<PositionRecord16>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions16.Add(new PositionRecord16
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition16
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions16 = positions16
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
                await _debrisCollection16.InsertManyAsync(newRecords);
            }
        }
    }

    /* public class TLECalculationServiceD16
     {
         private readonly IMongoCollection<SpaceObjectPosition16> _DebrisCollection16;
         private readonly IMongoCollection<TLEDebris> _tleCollection16;

         public TLECalculationServiceD16(IMongoDatabase database)
         {
             _DebrisCollection16 = database.GetCollection<SpaceObjectPosition16>("Positions16");
             _tleCollection16 = database.GetCollection<TLEDebris>("TLEDebris");
         }

         public void CalculateAndUpdatePositions16ForNextHour()
         {
             DateTime startTime = DateTime.UtcNow;
             DateTime endTime = startTime.AddHours(1);

             // Fetch the next 100 TLEs (skip the first 100, take the next 100)
             var tleList = _tleCollection16.Find(_ => true)
                                     .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
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

                     var positions16 = new List<PositionRecord16>();

                     // Generate position data for each minute in the next hour
                     for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                     {
                         var eci = sgp4.FindPosition(timestamp);
                         var geo = eci.ToGeodetic();

                         positions16.Add(new PositionRecord16
                         {
                             Timestamp = timestamp,
                             Latitude = geo.Latitude.Degrees,
                             Longitude = geo.Longitude.Degrees,
                             Altitude = geo.Altitude
                         });
                     }

                     // Upsert logic: Insert if not exists, update otherwise
                     var filter = Builders<SpaceObjectPosition16>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                     var update = Builders<SpaceObjectPosition16>.Update
                         .SetOnInsert(x => x.NoradId, tleData.NoradId)
                         .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                         .PushEach(x => x.Positions16, positions16);

                     _DebrisCollection16.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                 }
             }
         }
     }*/
    //17
    public class TLECalculationServiceD17
    {
        private readonly IMongoCollection<SpaceObjectPosition17> _debrisCollection17;
        private readonly IMongoCollection<TLEDebris> _tleCollection17;

        public TLECalculationServiceD17(IMongoDatabase database)
        {
            _debrisCollection17 = database.GetCollection<SpaceObjectPosition17>("Positions17");
            _tleCollection17 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public async Task CalculateAndUpdatePositions17ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear old data before inserting new positions**
            await _debrisCollection17.DeleteManyAsync(FilterDefinition<SpaceObjectPosition17>.Empty);

            // Fetch the next 100 TLEs (skip the first 600, take the next 100)
            var tleList = await _tleCollection17.Find(_ => true)
                                                .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                                .Skip(9100) // Skip the first 600 TLEs
                                                .Limit(1300) // Take the next 100 TLEs//////////////////////////////////////////////////1950
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition17>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions17 = new List<PositionRecord17>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions17.Add(new PositionRecord17
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition17
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions17 = positions17
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
                await _debrisCollection17.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceD17
    {
        private readonly IMongoCollection<SpaceObjectPosition17> _DebrisCollection17;
        private readonly IMongoCollection<TLEDebris> _tleCollection17;

        public TLECalculationServiceD17(IMongoDatabase database)
        {
            _DebrisCollection17 = database.GetCollection<SpaceObjectPosition17>("Positions17");
            _tleCollection17 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public void CalculateAndUpdatePositions17ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);




            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = _tleCollection17.Find(_ => true)
                                    .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
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

                    var positions17 = new List<PositionRecord17>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions17.Add(new PositionRecord17
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition17>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition17>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(" SpaceObjectPosition17.Positions17", positions17);
                    //.PushEach("Positions2", positions2);
                    //.PushEach(x => x.Positions2, positions2.ToArray());

                    //

                    _DebrisCollection17.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }

    }*/
    //18
    public class TLECalculationServiceD18
    {
        private readonly IMongoCollection<SpaceObjectPosition18> _debrisCollection18;
        private readonly IMongoCollection<TLEDebris> _tleCollection18;

        public TLECalculationServiceD18(IMongoDatabase database)
        {
            _debrisCollection18 = database.GetCollection<SpaceObjectPosition18>("Positions18");
            _tleCollection18 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public async Task CalculateAndUpdatePositions18ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear old data before inserting new positions**
            await _debrisCollection18.DeleteManyAsync(FilterDefinition<SpaceObjectPosition18>.Empty);

            // Fetch the next 100 TLEs (skip the first 700, take the next 100)
            var tleList = await _tleCollection18.Find(_ => true)
                                                .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
                                                .Skip(11050) // Skip the first 700 TLEs
                                                .Limit(1300) // Take the next 100 TLEs//////////////////////////////////////////////////////1950
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition18>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions18 = new List<PositionRecord18>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions18.Add(new PositionRecord18
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition18
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions18 = positions18
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
                await _debrisCollection18.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceD18
    {
        private readonly IMongoCollection<SpaceObjectPosition18> _DebrisCollection18;
        private readonly IMongoCollection<TLEDebris> _tleCollection18;

        public TLECalculationServiceD18(IMongoDatabase database)
        {
            _DebrisCollection18 = database.GetCollection<SpaceObjectPosition18>("Positions18");
            _tleCollection18 = database.GetCollection<TLEDebris>("TLEDebris");
        }

        public void CalculateAndUpdatePositions18ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);
            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = _tleCollection18.Find(_ => true)
                                    .Sort(Builders<TLEDebris>.Sort.Ascending(t => t.Timestamp))
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

                    var positions18 = new List<PositionRecord18>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions18.Add(new PositionRecord18
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition18>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition18>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(" SpaceObjectPosition18.Positions18", positions18);
                    //.PushEach(x => x.Positions3, positions3);

                    _DebrisCollection18.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }
    }*/
    //19
    public class TLECalculationServiceD19
    {
        private readonly IMongoCollection<SpaceObjectPosition19> _rocketCollection19;
        private readonly IMongoCollection<TLERockets> _tleCollection19;

        public TLECalculationServiceD19(IMongoDatabase database)
        {
            _rocketCollection19 = database.GetCollection<SpaceObjectPosition19>("Positions19");
            _tleCollection19 = database.GetCollection<TLERockets>("TLERockets");
        }

        public async Task CalculateAndUpdatePositions19ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear old data before inserting new positions**
            await _rocketCollection19.DeleteManyAsync(FilterDefinition<SpaceObjectPosition19>.Empty);

            // Fetch the first 100 TLEs, ordered by Timestamp
            var tleList = await _tleCollection19.Find(_ => true)
                                                .Sort(Builders<TLERockets>.Sort.Ascending(t => t.Timestamp))
                                                .Limit(2000)////////////////////////2200
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition19>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions19 = new List<PositionRecord19>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions19.Add(new PositionRecord19
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition19
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions19 = positions19
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
                await _rocketCollection19.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceD19
    {
        private readonly IMongoCollection<SpaceObjectPosition19> _RocketCollection19;
        private readonly IMongoCollection<TLERockets> _tleCollection19;

        public TLECalculationServiceD19(IMongoDatabase database)
        {
            _RocketCollection19 = database.GetCollection<SpaceObjectPosition19>("Positions19");
            _tleCollection19 = database.GetCollection<TLERockets>("TLERockets");
        }

        public void CalculateAndUpdatePositions19ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);
            // Fetch the first 100 TLEs, ordered by Timestamp
            var tleList = _tleCollection19.Find(_ => true)
                                        .Sort(Builders<TLERockets>.Sort.Ascending(t => t.Timestamp))
                                        .Limit(100)
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

                    var positions19 = new List<PositionRecord19>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions19.Add(new PositionRecord19
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition19>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition19>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(" SpaceObjectPosition19.Positions19", positions19);
                    //.PushEach(x => x.Positions3, positions3);

                    _RocketCollection19.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }
    }*/
    public class TLECalculationServiceD20
    {
        private readonly IMongoCollection<SpaceObjectPosition20> _unknownCollection20;
        private readonly IMongoCollection<TLEUnknown> _tleCollection20;

        public TLECalculationServiceD20(IMongoDatabase database)
        {
            _unknownCollection20 = database.GetCollection<SpaceObjectPosition20>("Positions20");
            _tleCollection20 = database.GetCollection<TLEUnknown>("TLEUnknown");
        }

        public async Task CalculateAndUpdatePositions20ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear old data before inserting new positions**
            await _unknownCollection20.DeleteManyAsync(FilterDefinition<SpaceObjectPosition20>.Empty);

            // Fetch the first 100 TLEs, ordered by Timestamp
            var tleList = await _tleCollection20.Find(_ => true)
                                                .Sort(Builders<TLEUnknown>.Sort.Ascending(t => t.Timestamp))
                                                .Limit(500)
                                                .ToListAsync();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition20>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions20 = new List<PositionRecord20>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions20.Add(new PositionRecord20
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition20
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions20 = positions20
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
                await _unknownCollection20.InsertManyAsync(newRecords);
            }
        }
    }

    /*public class TLECalculationServiceD20
    {
        private readonly IMongoCollection<SpaceObjectPosition20> _UnKnownCollection20;
        private readonly IMongoCollection<TLEUnknown> _tleCollection20;

        public TLECalculationServiceD20(IMongoDatabase database)
        {
            _UnKnownCollection20 = database.GetCollection<SpaceObjectPosition20>("Positions20");
            _tleCollection20 = database.GetCollection<TLEUnknown>("TLEUnknown");
        }

        public void CalculateAndUpdatePositions20ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // Fetch the first 100 TLEs, ordered by Timestamp
            var tleList = _tleCollection20.Find(_ => true)
                                        .Sort(Builders<TLEUnknown>.Sort.Ascending(t => t.Timestamp))
                                        .Limit(100)
                                        .ToList();
         

            if (!tleList.Any()) return;

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions20 = new List<PositionRecord20>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions20.Add(new PositionRecord20
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition20>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition20>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(x => x.Positions20, positions20);

                    _UnKnownCollection20.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }
    }*/

}