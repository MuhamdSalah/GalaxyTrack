using MongoDB.Driver;
using SpaceTrack.DAL;
using SpaceTrack.DAL.Model;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpaceTrack.Services
{
    public class PositionsService1
    {
        private readonly IMongoCollection<SpaceObjectPosition> _positionsCollection;
        private readonly IMongoCollection<SpaceObjectPosition2> _positionsCollection2;
        private readonly IMongoCollection<SpaceObjectPosition3> _positionsCollection3;
        private readonly IMongoCollection<SpaceObjectPosition4> _positionsCollection4;
        private readonly IMongoCollection<SpaceObjectPosition5> _positionsCollection5;

        public PositionsService1(MongoDbContext dbContext)
        {
            _positionsCollection = dbContext.Positions;
            _positionsCollection2 = dbContext.Positions2;
            _positionsCollection3 = dbContext.Positions3;
            _positionsCollection4 = dbContext.Positions4;
            _positionsCollection5 = dbContext.Positions5;
        }

        public async Task<byte[]> GetAllPositionsCompressedAsync()
        {
            var positions1 = await _positionsCollection.Find(_ => true).ToListAsync();
            var positions2 = await _positionsCollection2.Find(_ => true).ToListAsync();
            var positions3 = await _positionsCollection3.Find(_ => true).ToListAsync();
            var positions4 = await _positionsCollection4.Find(_ => true).ToListAsync();
            var positions5 = await _positionsCollection5.Find(_ => true).ToListAsync();

            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    AddJsonToZip(zipArchive, "positions1.json", positions1);
                    AddJsonToZip(zipArchive, "positions2.json", positions2);
                    AddJsonToZip(zipArchive, "positions3.json", positions3);
                    AddJsonToZip(zipArchive, "positions4.json", positions4);
                    AddJsonToZip(zipArchive, "positions5.json", positions5);
                }

                return memoryStream.ToArray();
            }
        }

        private void AddJsonToZip<T>(ZipArchive zipArchive, string fileName, List<T> data)
        {
            var entry = zipArchive.CreateEntry(fileName, CompressionLevel.Optimal);
            using (var entryStream = entry.Open())
            using (var writer = new StreamWriter(entryStream, Encoding.UTF8))
            {
                string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                writer.Write(jsonData);
            }
        }
    }
}

/////////////////////////////////////////////////////////////////////
//using MongoDB.Driver;
//using SpaceTrack.DAL;
//using SpaceTrack.DAL.Model;
//using System.Collections.Generic;
//using System.IO;
//using System.IO.Compression;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace SpaceTrack.Services
//{
//    public class PositionsService1
//    {
//        private readonly IMongoCollection<SpaceObjectPosition> _positionsCollection;

//        public PositionsService1(MongoDbContext dbContext)
//        {
//            _positionsCollection = dbContext.Positions; // Ensure this maps to the correct collection
//        }

//        public async Task<byte[]> GetAllPositionsCompressedAsync()
//        {
//            var positions = await _positionsCollection.Find(_ => true).ToListAsync();

//            // Convert positions data to JSON
//            string jsonData = JsonSerializer.Serialize(positions, new JsonSerializerOptions { WriteIndented = true });

//            // Compress the JSON data into a ZIP file
//            using (var memoryStream = new MemoryStream())
//            {
//                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
//                {
//                    var zipEntry = zipArchive.CreateEntry("positions.json", CompressionLevel.Optimal);
//                    using (var entryStream = zipEntry.Open())
//                    using (var writer = new StreamWriter(entryStream, Encoding.UTF8))
//                    {
//                        await writer.WriteAsync(jsonData);
//                    }
//                }

//                return memoryStream.ToArray(); // Return the ZIP file as a byte array
//            }
//        }
//    }
//}
//////////////////////////
//using MongoDB.Driver;
//using SpaceTrack.DAL;
//using SpaceTrack.DAL.Model;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace SpaceTrack.Services
//{
//    public class PositionsService1
//    {
//        private readonly IMongoCollection<SpaceObjectPosition> _positionsCollection;
//        private readonly IMongoCollection<SpaceObjectPosition2> _positionsCollection2;
//        private readonly IMongoCollection<SpaceObjectPosition3> _positionsCollection3;
//        private readonly IMongoCollection<SpaceObjectPosition4> _positionsCollection4;
//        private readonly IMongoCollection<SpaceObjectPosition5> _positionsCollection5;

//        public PositionsService1(MongoDbContext dbContext)
//        {
//            _positionsCollection = dbContext.Positions; // Assuming 'Positions' is the correct collection.
//            _positionsCollection2 = dbContext.Positions2;
//            _positionsCollection3 = dbContext.Positions3;
//            _positionsCollection4 = dbContext.Positions4;
//            _positionsCollection5 = dbContext.Positions5;

//        }

//        public async Task<List<SpaceObjectPosition>> GetAllPositionsAsync()
//        {
//            return await _positionsCollection.Find(_ => true).ToListAsync();
//        }
//        public async Task<List<SpaceObjectPosition2>> GetAllPositions2Async()
//        {
//            return await _positionsCollection2.Find(_ => true).ToListAsync();
//        }
//        public async Task<List<SpaceObjectPosition3>> GetAllPositions3Async()
//        {
//            return await _positionsCollection3.Find(_ => true).ToListAsync();
//        }
//        public async Task<List<SpaceObjectPosition4>> GetAllPositions4Async()
//        {
//            return await _positionsCollection4.Find(_ => true).ToListAsync();
//        }
//        public async Task<List<SpaceObjectPosition5>> GetAllPositions5Async()
//        {
//            return await _positionsCollection5.Find(_ => true).ToListAsync();
//        }


//    }
//}
