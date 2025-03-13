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
    public class PositionsService2
    {
        private readonly IMongoCollection<SpaceObjectPosition6> _positionsCollection6;
        private readonly IMongoCollection<SpaceObjectPosition7> _positionsCollection7;
        private readonly IMongoCollection<SpaceObjectPosition8> _positionsCollection8;
        private readonly IMongoCollection<SpaceObjectPosition9> _positionsCollection9;
        private readonly IMongoCollection<SpaceObjectPosition10> _positionsCollection10;

        public PositionsService2(MongoDbContext dbContext)
        {
            _positionsCollection6 = dbContext.Positions6;
            _positionsCollection7 = dbContext.Positions7;
            _positionsCollection8 = dbContext.Positions8;
            _positionsCollection9 = dbContext.Positions9;
            _positionsCollection10 = dbContext.Positions10;
        }

        public async Task<byte[]> GetAllPositionsCompressedAsync()
        {
            var positions6 = await _positionsCollection6.Find(_ => true).ToListAsync();
            var positions7 = await _positionsCollection7.Find(_ => true).ToListAsync();
            var positions8 = await _positionsCollection8.Find(_ => true).ToListAsync();
            var positions9 = await _positionsCollection9.Find(_ => true).ToListAsync();
            var positions10 = await _positionsCollection10.Find(_ => true).ToListAsync();

            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    AddJsonToZip(zipArchive, "positions6.json", positions6);
                    AddJsonToZip(zipArchive, "positions7.json", positions7);
                    AddJsonToZip(zipArchive, "positions8.json", positions8);
                    AddJsonToZip(zipArchive, "positions9.json", positions9);
                    AddJsonToZip(zipArchive, "positions10.json", positions10);
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