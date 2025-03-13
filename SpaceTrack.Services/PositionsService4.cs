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
    public class PositionsService4
    {
        private readonly IMongoCollection<SpaceObjectPosition16> _positionsCollection16;
        private readonly IMongoCollection<SpaceObjectPosition17> _positionsCollection17;
        private readonly IMongoCollection<SpaceObjectPosition18> _positionsCollection18;
        private readonly IMongoCollection<SpaceObjectPosition19> _positionsCollection19;
        private readonly IMongoCollection<SpaceObjectPosition20> _positionsCollection20;

        public PositionsService4(MongoDbContext dbContext)
        {
            _positionsCollection16 = dbContext.Positions16;
            _positionsCollection17 = dbContext.Positions17;
            _positionsCollection18 = dbContext.Positions18;
            _positionsCollection19 = dbContext.Positions19;
            _positionsCollection20 = dbContext.Positions20;
        }

        public async Task<byte[]> GetAllPositionsCompressedAsync()
        {
            var positions16 = await _positionsCollection16.Find(_ => true).ToListAsync();
            var positions17 = await _positionsCollection17.Find(_ => true).ToListAsync();
            var positions18 = await _positionsCollection18.Find(_ => true).ToListAsync();
            var positions19 = await _positionsCollection19.Find(_ => true).ToListAsync();
            var positions20 = await _positionsCollection20.Find(_ => true).ToListAsync();

            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    AddJsonToZip(zipArchive, "positions16.json", positions16);
                    AddJsonToZip(zipArchive, "positions17.json", positions17);
                    AddJsonToZip(zipArchive, "positions18.json", positions18);
                    AddJsonToZip(zipArchive, "positions19.json", positions19);
                    AddJsonToZip(zipArchive, "positions20.json", positions20);
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
