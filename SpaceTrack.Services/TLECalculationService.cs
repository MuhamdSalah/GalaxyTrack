using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using SpaceTrack.DAL.Model;
using SGPdotNET.Propagation;
using SGPdotNET.TLE;

namespace SpaceTrack.Services
{
    public class TLECalculationService
    {
        private readonly IMongoCollection<SpaceObjectPosition> _satelliteCollection;
        private readonly IMongoCollection<TLEPayloads> _tleCollection;

        public TLECalculationService(IMongoDatabase database)
        {
            _satelliteCollection = database.GetCollection<SpaceObjectPosition>("Positions1");
            _tleCollection = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

       
        public async Task CalculateAndUpdatePositionsForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // Delete all existing records in the Positions1 collection before inserting new data
            _satelliteCollection.DeleteMany(Builders<SpaceObjectPosition>.Filter.Empty);

            // Fetch the first 100 TLEs, ordered by Timestamp
            var tleList = _tleCollection.Find(_ => true)
                                        .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                        .Limit(1300)
                                        .ToList();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions = new List<PositionRecord>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions.Add(new PositionRecord
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions = positions
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
                _satelliteCollection.InsertMany(newRecords);
            }
        }

        
    }
    //2
    public class TLECalculationService2
    {
        private readonly IMongoCollection<SpaceObjectPosition2> _satelliteCollection2;
        private readonly IMongoCollection<TLEPayloads> _tleCollection2;

        public TLECalculationService2(IMongoDatabase database)
        {
            _satelliteCollection2 = database.GetCollection<SpaceObjectPosition2>("Positions2");
            _tleCollection2 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }
        public async Task CalculateAndUpdatePositions2ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // Delete all existing records in the Positions2 collection before inserting new data
            _satelliteCollection2.DeleteMany(Builders<SpaceObjectPosition2>.Filter.Empty);

            // Fetch the next 1300 TLEs (skip the first 1300, take the next 1300)
            var tleList = _tleCollection2.Find(_ => true)
                                         .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                         .Skip(1300)
                                         .Limit(1300)
                                         .ToList();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition2>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions2 = new List<PositionRecord2>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions2.Add(new PositionRecord2
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition2
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions2 = positions2
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
                _satelliteCollection2.InsertMany(newRecords);
            }
        }

        

    }
    //3
    public class TLECalculationService3
    {
        private readonly IMongoCollection<SpaceObjectPosition3> _satelliteCollection3;
        private readonly IMongoCollection<TLEPayloads> _tleCollection3;

        public TLECalculationService3(IMongoDatabase database)
        {
            _satelliteCollection3 = database.GetCollection<SpaceObjectPosition3>("Positions3");
            _tleCollection3 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }
        public async Task CalculateAndUpdatePositions3ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // Delete all existing records in the Positions3 collection before inserting new data
            _satelliteCollection3.DeleteMany(Builders<SpaceObjectPosition3>.Filter.Empty);

            // Fetch the next 1300 TLEs (skip the first 2600, take the next 1300)
            var tleList = _tleCollection3.Find(_ => true)
                                         .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                         .Skip(2600)
                                         .Limit(1300)
                                         .ToList();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition3>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions3 = new List<PositionRecord3>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions3.Add(new PositionRecord3
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition3
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions3 = positions3
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
                _satelliteCollection3.InsertMany(newRecords);
            }
        }

    }
    //4
    public class TLECalculationService4
    {
        private readonly IMongoCollection<SpaceObjectPosition4> _satelliteCollection4;
        private readonly IMongoCollection<TLEPayloads> _tleCollection4;

        public TLECalculationService4(IMongoDatabase database)
        {
            _satelliteCollection4 = database.GetCollection<SpaceObjectPosition4>("Positions4");
            _tleCollection4 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions4ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the entire collection before inserting new data**
            _satelliteCollection4.DeleteMany(FilterDefinition<SpaceObjectPosition4>.Empty);

            // Fetch the next 1300 TLEs (skip the first 3900, take the next 1300)
            var tleList = _tleCollection4.Find(_ => true)
                                         .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                         .Skip(3900)
                                         .Limit(1300)
                                         .ToList();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition4>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions4 = new List<PositionRecord4>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions4.Add(new PositionRecord4
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition4
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions4 = positions4
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
                _satelliteCollection4.InsertMany(newRecords);
            }
        }
    }

    
    public class TLECalculationService5
    {
        private readonly IMongoCollection<SpaceObjectPosition5> _satelliteCollection5;
        private readonly IMongoCollection<TLEPayloads> _tleCollection5;

        public TLECalculationService5(IMongoDatabase database)
        {
            _satelliteCollection5 = database.GetCollection<SpaceObjectPosition5>("Positions5");
            _tleCollection5 = database.GetCollection<TLEPayloads>("TLEPayloads");
        }

        public async Task CalculateAndUpdatePositions5ForNextHourAsync()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // **Clear the entire collection before inserting new data**
            _satelliteCollection5.DeleteMany(FilterDefinition<SpaceObjectPosition5>.Empty);

            // Fetch the next 1300 TLEs (skip the first 5200, take the next 1300)
            var tleList = _tleCollection5.Find(_ => true)
                                         .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                         .Skip(5200)
                                         .Limit(1300)
                                         .ToList();

            if (!tleList.Any()) return;

            var newRecords = new List<SpaceObjectPosition5>();

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions5 = new List<PositionRecord5>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions5.Add(new PositionRecord5
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Prepare new record
                    var newRecord = new SpaceObjectPosition5
                    {
                        NoradId = tleData.NoradId,
                        ObjectName = tleData.ObjectName,
                        Positions5 = positions5
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
                _satelliteCollection5.InsertMany(newRecords);
            }
        }
    }
   

}
/*public void CalculateAndUpdatePositionsForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);

            // Fetch the first 100 TLEs, ordered by Timestamp
            var tleList = _tleCollection.Find(_ => true)
                                        .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                        .Limit(1300)
                                        .ToList();

            if (!tleList.Any()) return;

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions = new List<PositionRecord>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions.Add(new PositionRecord
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                        .PushEach(x => x.Positions, positions);

                    _satelliteCollection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }*/
/*public class TLECalculationService5
   {
       private readonly IMongoCollection<SpaceObjectPosition5> _satelliteCollection5;
       private readonly IMongoCollection<TLEPayloads> _tleCollection5;

       public TLECalculationService5(IMongoDatabase database)
       {
           _satelliteCollection5 = database.GetCollection<SpaceObjectPosition5>("Positions5");
           _tleCollection5 = database.GetCollection<TLEPayloads>("TLEPayloads");
       }

       public void CalculateAndUpdatePositions5ForNextHour()
       {
           DateTime startTime = DateTime.UtcNow;
           DateTime endTime = startTime.AddHours(1);

           var tleList = _tleCollection5.Find(_ => true)
                                    .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                    .Skip(5200) // Skip the first 5200 TLEs
                                    .Limit(1300) // Take the next 1300 TLEs
                                    .ToList();

           if (!tleList.Any()) return;

           foreach (var tleData in tleList)
           {
               try
               {
                   var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                   var sgp4 = new Sgp4(tle);

                   var positions = new List<PositionRecord5>();

                   // Generate position data for each minute in the next hour
                   for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                   {
                       var eci = sgp4.FindPosition(timestamp);
                       var geo = eci.ToGeodetic();

                       positions.Add(new PositionRecord5
                       {
                           Timestamp = timestamp,
                           Latitude = geo.Latitude.Degrees,
                           Longitude = geo.Longitude.Degrees,
                           Altitude = geo.Altitude
                       });
                   }

                   // Upsert logic: Insert if not exists, update otherwise
                   var filter = Builders<SpaceObjectPosition5>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                   var update = Builders<SpaceObjectPosition5>.Update
                       .SetOnInsert(x => x.NoradId, tleData.NoradId)
                       .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                       .PushEach(x => x.Positions5, positions);

                   _satelliteCollection5.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
               }
               catch (Exception ex)
               {
                   Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
               }
           }
       }
   }*/
/* public class TLECalculationService4
     {
         private readonly IMongoCollection<SpaceObjectPosition4> _satelliteCollection4;
         private readonly IMongoCollection<TLEPayloads> _tleCollection4;

         public TLECalculationService4(IMongoDatabase database)
         {
             _satelliteCollection4 = database.GetCollection<SpaceObjectPosition4>("Positions4");
             _tleCollection4 = database.GetCollection<TLEPayloads>("TLEPayloads");
         }

        public void CalculateAndUpdatePositions4ForNextHour()
         {
             DateTime startTime = DateTime.UtcNow;
             DateTime endTime = startTime.AddHours(1);
             // Fetch the next 100 TLEs (skip the first 100, take the next 100)
             var tleList = _tleCollection4.Find(_ => true)
                                     .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                     .Skip(3900) // Skip the first 3900 TLEs
                                     .Limit(1300) // Take the next 100 TLEs
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

                     var positions4 = new List<PositionRecord4>();

                     // Generate position data for each minute in the next hour
                     for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                     {
                         var eci = sgp4.FindPosition(timestamp);
                         var geo = eci.ToGeodetic();

                         positions4.Add(new PositionRecord4
                         {
                             Timestamp = timestamp,
                             Latitude = geo.Latitude.Degrees,
                             Longitude = geo.Longitude.Degrees,
                             Altitude = geo.Altitude
                         });
                     }

                     // Upsert logic: Insert if not exists, update otherwise
                     var filter = Builders<SpaceObjectPosition4>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                     var update = Builders<SpaceObjectPosition4>.Update
                         .SetOnInsert(x => x.NoradId, tleData.NoradId)
                         .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                         .PushEach(x => x.Positions4, positions4);
                         //.PushEach(" SpaceObjectPosition4.Positions4", positions4);


                     _satelliteCollection4.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                 }
             }
         }*/

/*  public void CalculateAndUpdatePositions3ForNextHour()
  {
      DateTime startTime = DateTime.UtcNow;
      DateTime endTime = startTime.AddHours(1);
      // Fetch the next 100 TLEs (skip the first 100, take the next 100)
      var tleList = _tleCollection3.Find(_ => true)
                              .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                              .Skip(2600) // Skip the first 2600 TLEs
                              .Limit(1300) // Take the next 1300 TLEs
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

              var positions3 = new List<PositionRecord3>();

              // Generate position data for each minute in the next hour
              for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
              {
                  var eci = sgp4.FindPosition(timestamp);
                  var geo = eci.ToGeodetic();

                  positions3.Add(new PositionRecord3
                  {
                      Timestamp = timestamp,
                      Latitude = geo.Latitude.Degrees,
                      Longitude = geo.Longitude.Degrees,
                      Altitude = geo.Altitude
                  });
              }

              // Upsert logic: Insert if not exists, update otherwise
              var filter = Builders<SpaceObjectPosition3>.Filter.Eq(x => x.NoradId, tleData.NoradId);
              var update = Builders<SpaceObjectPosition3>.Update
                  .SetOnInsert(x => x.NoradId, tleData.NoradId)
                  .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                .PushEach(x => x.Positions3, positions3);
              //.PushEach(" SpaceObjectPosition3.Positions3", positions3);//true
              //.PushEach(x => x.Positions3, positions3);

              _satelliteCollection3.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
          }
          catch (Exception ex)
          {
              Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
          }
      }
  }*/
/*public void CalculateAndUpdatePositions2ForNextHour()
        {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddHours(1);




            // Fetch the next 100 TLEs (skip the first 100, take the next 100)
            var tleList = _tleCollection2.Find(_ => true)
                                    .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
                                    .Skip(1300) // Skip the first 1300 TLEs
                                    .Limit(1300) // Take the next 1300 TLEs
                                    .ToList();

            if (!tleList.Any()) return;

            foreach (var tleData in tleList)
            {
                try
                {
                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
                    var sgp4 = new Sgp4(tle);

                    var positions2 = new List<PositionRecord2>();

                    // Generate position data for each minute in the next hour
                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
                    {
                        var eci = sgp4.FindPosition(timestamp);
                        var geo = eci.ToGeodetic();

                        positions2.Add(new PositionRecord2
                        {
                            Timestamp = timestamp,
                            Latitude = geo.Latitude.Degrees,
                            Longitude = geo.Longitude.Degrees,
                            Altitude = geo.Altitude
                        });
                    }

                    // Upsert logic: Insert if not exists, update otherwise
                    var filter = Builders<SpaceObjectPosition2>.Filter.Eq(x => x.NoradId, tleData.NoradId);
                    var update = Builders<SpaceObjectPosition2>.Update
                        .SetOnInsert(x => x.NoradId, tleData.NoradId)
                        .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
                    .PushEach(x => x.Positions2, positions2);
                    //.PushEach("Positions2", positions2);
                    //.PushEach(" SpaceObjectPosition2.Positions2", positions2); //true

                    //.PushEach(x => x.Positions2, positions2.ToArray());

                    //

                    _satelliteCollection2.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
                }
            }
        }*/
//////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//public class TLECalculationService2
//{
//    private readonly IMongoCollection<SpaceObjectPosition2> _satelliteCollection;
//    private readonly IMongoCollection<TLEPayloads> _tleCollection;

//    public TLECalculationService2(IMongoDatabase database)
//    {
//        _satelliteCollection = database.GetCollection<SpaceObjectPosition2>("SatelliteDataPackages");
//        _tleCollection = database.GetCollection<TLEPayloads>("TLEPayloads");
//    }

//    public void CalculateAndUpdatePositions2ForNextHour()
//    {
//        DateTime startTime = DateTime.UtcNow;
//        DateTime endTime = startTime.AddHours(1);

//        // Fetch the first 100 TLEs, ordered by Timestamp
//        var tleList = _tleCollection.Find(_ => true)
//                                    .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
//                                    .Limit(100)
//                                    .ToList();

//        if (!tleList.Any()) return;

//        foreach (var tleData in tleList)
//        {
//            try
//            {
//                var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
//                var sgp4 = new Sgp4(tle);

//                var positions2 = new List<PositionRecord2>();

//                // Generate position data for each minute in the next hour
//                for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
//                {
//                    var eci = sgp4.FindPosition(timestamp);
//                    var geo = eci.ToGeodetic();

//                    positions2.Add(new PositionRecord2
//                    {
//                        Timestamp = timestamp,
//                        Latitude = geo.Latitude.Degrees,
//                        Longitude = geo.Longitude.Degrees,
//                        Altitude = geo.Altitude
//                    });
//                }

//                // Upsert logic: Insert if not exists, update otherwise
//                var filter = Builders<SpaceObjectPosition2>.Filter.Eq(x => x.NoradId, tleData.NoradId);
//                var update = Builders<SpaceObjectPosition2>.Update
//                    .SetOnInsert(x => x.NoradId, tleData.NoradId)
//                    .SetOnInsert(x => x.ObjectName, tleData.ObjectName)
//                    .PushEach("Positions2", positions2);
//                    //.PushEach(x => x.Positions2, positions2);

//                _satelliteCollection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
//            }
//        }
//    }
//}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////////////////////worka
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using MongoDB.Driver;
//using SpaceTrack.DAL.Model;
//using SGPdotNET.Propagation;
//using SGPdotNET.TLE;

//namespace SpaceTrack.Services
//{
//    public class TLECalculationService
//    {
//        private readonly IMongoCollection<SpaceObjectPosition> _positionsCollection;
//        private readonly IMongoCollection<TLEPayloads> _tleCollection;

//        public TLECalculationService(IMongoDatabase database)
//        {
//            _positionsCollection = database.GetCollection<SpaceObjectPosition>("SpaceObjectPositions");
//            _tleCollection = database.GetCollection<TLEPayloads>("TLEPayloads");
//        }

//        public void CalculateAndUpdatePositionsForNextHour()
//        {
//            // Get the current UTC time range (Next 1 hour)
//            DateTime startTime = DateTime.UtcNow;
//            DateTime endTime = startTime.AddHours(1);

//            // Fetch the first 100 TLEs, ordered by Timestamp
//            var tleList = _tleCollection.Find(_ => true)
//                                        .Sort(Builders<TLEPayloads>.Sort.Ascending(t => t.Timestamp))
//                                        .Limit(1000)
//                                        .ToList();

//            if (!tleList.Any()) return;

//            foreach (var tleData in tleList)
//            {
//                try
//                {
//                    // Parse the TLE
//                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
//                    var sgp4 = new Sgp4(tle);

//                    var positions = new List<SpaceObjectPosition>();

//                    // Generate position data for each minute in the next hour
//                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
//                    {
//                        var eci = sgp4.FindPosition(timestamp);
//                        var geo = eci.ToGeodetic();

//                        positions.Add(new SpaceObjectPosition
//                        {
//                            NoradId = tleData.NoradId,
//                            ObjectName = tleData.ObjectName,
//                            Timestamp = timestamp,
//                            Latitude = geo.Latitude.Degrees,
//                            Longitude = geo.Longitude.Degrees,
//                            Altitude = geo.Altitude
//                        });
//                    }

//                    // Remove old positions within this hour for this satellite
//                    var deleteFilter = Builders<SpaceObjectPosition>.Filter.And(
//                        Builders<SpaceObjectPosition>.Filter.Eq(x => x.NoradId, tleData.NoradId),
//                        Builders<SpaceObjectPosition>.Filter.Gte(x => x.Timestamp, startTime),
//                        Builders<SpaceObjectPosition>.Filter.Lte(x => x.Timestamp, endTime)
//                    );

//                    _positionsCollection.DeleteMany(deleteFilter);

//                    // Insert new positions
//                    if (positions.Any())
//                    {
//                        _positionsCollection.InsertMany(positions);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error processing TLE for {tleData.NoradId}: {ex.Message}");
//                }
//            }
//        }
//    }
//}
///////////////////////////////////////////////////////////////////////////////////////////////////worka

/////////////////////////////////////////////////////////////////////////////////////////////////////////////to fetch the first 100
////using SGP4;  // Make sure to include this!
////using SGP4.TLE;
//using System;
//using MongoDB.Driver;
//using SpaceTrack.DAL.Model;
//using SGPdotNET.Propagation;
//using SGPdotNET.TLE;

//namespace SpaceTrack.Services
//{
//    public class TLECalculationService
//    {
//        private readonly IMongoCollection<SpaceObjectPosition> _positionsCollection;

//        public TLECalculationService(IMongoDatabase database)
//        {
//            _positionsCollection = database.GetCollection<SpaceObjectPosition>("SpaceObjectPositions");
//        }

//        public SpaceObjectPosition CalculateAndStorePosition(string firstLine, string secondLine, DateTime? timestamp = null)
//        {
//            // If no timestamp is provided, use the current execution time (UTC)
//            DateTime tleTime = timestamp ?? DateTime.UtcNow;

//            try
//            {

//                // Parse the TLE
//                var tle = new Tle(firstLine, secondLine);

//                // Create SGP4 Propagator
//                var sgp4 = new Sgp4(tle);

//                // Propagate to the requested time
//                var eci = sgp4.FindPosition(tleTime);

//                // Convert ECI to Geodetic (Lat, Lon, Alt)
//                var geo = eci.ToGeodetic();

//                // Create position object
//                var position = new SpaceObjectPosition
//                {
//                    ObjectName = tle.Name,
//                    Timestamp = tleTime,
//                    Latitude = geo.Latitude.Degrees,  // Degrees
//                    Longitude = geo.Longitude.Degrees, // Degrees
//                    Altitude = geo.Altitude    // Kilometers
//                };

//                // Save to MongoDB
//                _positionsCollection.InsertOne(position);

//                return position;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error calculating position: {ex.Message}");
//                return null;
//            }
//        }
//    }
//}
////////////////////////////////////////////////////////////////////////////////////////work for the 100 to post
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using MongoDB.Driver;
//using SpaceTrack.DAL.Model;
//using SGPdotNET.Propagation;
//using SGPdotNET.TLE;

//namespace SpaceTrack.Services
//{
//    public class TLECalculationService
//    {
//        private readonly IMongoCollection<SpaceObjectPosition> _positionsCollection;
//        private readonly IMongoCollection<TLEPayloads> _tleCollection;

//        public TLECalculationService(IMongoDatabase database)
//        {
//            _positionsCollection = database.GetCollection<SpaceObjectPosition>("SpaceObjectPositions");
//            _tleCollection = database.GetCollection<TLEPayloads>("TLEPayloads");
//        }

//        public void CalculateAndStorePositionsForNextHour()
//        {
//            // Get the current UTC time
//            DateTime startTime = DateTime.UtcNow;
//            DateTime endTime = startTime.AddHours(1);

//            // Fetch 100 TLEs from the database
//            var tleList = _tleCollection.Find(_ => true).Limit(100).ToList();

//            if (!tleList.Any()) return;

//            var positionsToInsert = new List<SpaceObjectPosition>();

//            foreach (var tleData in tleList)
//            {
//                try
//                {
//                    // Parse the TLE
//                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);

//                    // Create SGP4 Propagator
//                    var sgp4 = new Sgp4(tle);

//                    // Calculate positions every minute for the next hour
//                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
//                    {
//                        var eci = sgp4.FindPosition(timestamp);
//                        var geo = eci.ToGeodetic();

//                        var position = new SpaceObjectPosition
//                        {
//                            NoradId = tleData.NoradId,
//                            ObjectName = tleData.ObjectName,
//                            Timestamp = timestamp,
//                            Latitude = geo.Latitude.Degrees,
//                            Longitude = geo.Longitude.Degrees,
//                            Altitude = geo.Altitude
//                        };

//                        positionsToInsert.Add(position);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error processing TLE for {tleData.ObjectName}: {ex.Message}");
//                }
//            }

//            // Insert all positions in bulk
//            if (positionsToInsert.Any())
//            {
//                _positionsCollection.InsertMany(positionsToInsert);
//            }
//        }
//    }
//}

////////////////////////////////////////////////////////////////////////////////////////work for the 100
///////////////////////////////////////////to update
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using MongoDB.Driver;
//using SpaceTrack.DAL.Model;
//using SGPdotNET.Propagation;
//using SGPdotNET.TLE;

//namespace SpaceTrack.Services
//{
//    public class TLECalculationService
//    {
//        private readonly IMongoCollection<SpaceObjectPosition> _positionsCollection;
//        private readonly IMongoCollection<TLEPayloads> _tleCollection;

//        public TLECalculationService(IMongoDatabase database)
//        {
//            _positionsCollection = database.GetCollection<SpaceObjectPosition>("SpaceObjectPositions");
//            _tleCollection = database.GetCollection<TLEPayloads>("TLEPayloads");
//        }

//        public void CalculateAndUpdatePositionsForNextHour()
//        {
//            // Get the current UTC time range (Next 1 hour)
//            DateTime startTime = DateTime.UtcNow;
//            DateTime endTime = startTime.AddHours(1);

//            // Fetch 100 TLEs from the database
//            var tleList = _tleCollection.Find(_ => true).Limit(100).ToList();
//            if (!tleList.Any()) return;

//            foreach (var tleData in tleList)
//            {
//                try
//                {
//                    // Parse the TLE
//                    var tle = new Tle(tleData.FirstLine, tleData.SecondLine);
//                    var sgp4 = new Sgp4(tle);

//                    var positions = new List<SpaceObjectPosition>();

//                    // Generate position data for each minute in the next hour
//                    for (DateTime timestamp = startTime; timestamp <= endTime; timestamp = timestamp.AddMinutes(1))
//                    {
//                        var eci = sgp4.FindPosition(timestamp);
//                        var geo = eci.ToGeodetic();

//                        positions.Add(new SpaceObjectPosition
//                        {
//                            NoradId = tleData.NoradId,
//                            ObjectName = tleData.ObjectName,
//                            Timestamp = timestamp,
//                            Latitude = geo.Latitude.Degrees,
//                            Longitude = geo.Longitude.Degrees,
//                            Altitude = geo.Altitude
//                        });
//                    }

//                    // Remove old positions within this hour for this satellite
//                    var deleteFilter = Builders<SpaceObjectPosition>.Filter.And(
//                        Builders<SpaceObjectPosition>.Filter.Eq(x => x.NoradId, tleData.NoradId),
//                        Builders<SpaceObjectPosition>.Filter.Gte(x => x.Timestamp, startTime),
//                        Builders<SpaceObjectPosition>.Filter.Lte(x => x.Timestamp, endTime)
//                    );

//                    _positionsCollection.DeleteMany(deleteFilter);

//                    // Insert new positions
//                    if (positions.Any())
//                    {
//                        _positionsCollection.InsertMany(positions);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error processing TLE for {tleData.ObjectName}: {ex.Message}");
//                }
//            }
//        }
//    }
//}

//////////////////////////////////////////////////////////////////////////////////////to update
/////////////////////////////////////////////////////////////////////////////////////////////////////////////to fetch the first 100