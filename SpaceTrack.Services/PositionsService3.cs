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
    public class PositionsService3
    {
        private readonly IMongoCollection<SpaceObjectPosition11> _positionsCollection11;
        private readonly IMongoCollection<SpaceObjectPosition12> _positionsCollection12;
        private readonly IMongoCollection<SpaceObjectPosition13> _positionsCollection13;
        private readonly IMongoCollection<SpaceObjectPosition14> _positionsCollection14;
        private readonly IMongoCollection<SpaceObjectPosition15> _positionsCollection15;

        public PositionsService3(MongoDbContext dbContext)
        {
            _positionsCollection11 = dbContext.Positions11;
            _positionsCollection12 = dbContext.Positions12;
            _positionsCollection13 = dbContext.Positions13;
            _positionsCollection14 = dbContext.Positions14;
            _positionsCollection15 = dbContext.Positions15;
        }

        public async Task<byte[]> GetAllPositionsCompressedAsync()
        {
            var positions11 = await _positionsCollection11.Find(_ => true).ToListAsync();
            var positions12 = await _positionsCollection12.Find(_ => true).ToListAsync();
            var positions13 = await _positionsCollection13.Find(_ => true).ToListAsync();
            var positions14= await _positionsCollection14.Find(_ => true).ToListAsync();
            var positions15 = await _positionsCollection15.Find(_ => true).ToListAsync();

            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    AddJsonToZip(zipArchive, "positions11.json", positions11);
                    AddJsonToZip(zipArchive, "positions12.json", positions12);
                    AddJsonToZip(zipArchive, "positions13.json", positions13);
                    AddJsonToZip(zipArchive, "positions14.json", positions14);
                    AddJsonToZip(zipArchive, "positions15.json", positions15);
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
