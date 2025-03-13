using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SpaceTrack.DAL.Model;


namespace SpaceTrack.DAL
{
     public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("SpaceTrack");
        }
        //for1
        public IMongoCollection<SpaceObjectPosition> Positions => _database.GetCollection<SpaceObjectPosition>("Positions1");

        public IMongoCollection<SpaceObjectPosition2> Positions2 => _database.GetCollection<SpaceObjectPosition2>("Positions2"); 
        public IMongoCollection<SpaceObjectPosition3> Positions3 => _database.GetCollection<SpaceObjectPosition3>("Positions3");
        public IMongoCollection<SpaceObjectPosition4> Positions4 => _database.GetCollection<SpaceObjectPosition4>("Positions4");

        public IMongoCollection<SpaceObjectPosition5>Positions5 => _database.GetCollection<SpaceObjectPosition5>("Positions5");
        public IMongoCollection<SpaceObjectPosition6> Positions6 => _database.GetCollection<SpaceObjectPosition6>("Positions6");
        public IMongoCollection<SpaceObjectPosition7> Positions7 => _database.GetCollection<SpaceObjectPosition7>("Positions7");
        public IMongoCollection<SpaceObjectPosition8> Positions8 => _database.GetCollection<SpaceObjectPosition8>("Positions8");

        public IMongoCollection<SpaceObjectPosition9> Positions9 => _database.GetCollection<SpaceObjectPosition9>("Positions9");
        public IMongoCollection<SpaceObjectPosition10> Positions10=> _database.GetCollection<SpaceObjectPosition10>("Positions10");
        public IMongoCollection<SpaceObjectPosition11> Positions11 => _database.GetCollection<SpaceObjectPosition11>("Positions11");
        public IMongoCollection<SpaceObjectPosition12> Positions12 => _database.GetCollection<SpaceObjectPosition12>("Positions12");

        public IMongoCollection<SpaceObjectPosition13> Positions13 => _database.GetCollection<SpaceObjectPosition13>("Positions13");
        public IMongoCollection<SpaceObjectPosition14> Positions14 => _database.GetCollection<SpaceObjectPosition14>("Positions14");

        public IMongoCollection<SpaceObjectPosition15> Positions15 => _database.GetCollection<SpaceObjectPosition15>("Positions15");

        public IMongoCollection<SpaceObjectPosition16> Positions16 => _database.GetCollection<SpaceObjectPosition16>("Positions16");

        public IMongoCollection<SpaceObjectPosition17> Positions17 => _database.GetCollection<SpaceObjectPosition17>("Positions17");
        public IMongoCollection<SpaceObjectPosition18> Positions18 => _database.GetCollection<SpaceObjectPosition18>("Positions18");


        public IMongoCollection<SpaceObjectPosition19> Positions19 => _database.GetCollection<SpaceObjectPosition19>("Positions19");

        public IMongoCollection<SpaceObjectPosition20> Positions20 => _database.GetCollection<SpaceObjectPosition20>("Positions20");


        //public IMongoCollection<SatelliteDataPackage> SatelliteDataPackages => _database.GetCollection<SatelliteDataPackage>("SatelliteDataPackages");

        public IMongoCollection<TLEPayloads> TLEPAYLOADS => _database.GetCollection<TLEPayloads>("TLEPayloads");

        //for1
        //for try 100
        public IMongoCollection<TLEPosition> TLEPositions => _database.GetCollection<TLEPosition>("TLEPositions");
        //for try 100
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<ResetPasswordRequest> ResetPasswordRequests => _database.GetCollection<ResetPasswordRequest>("ResetPasswordRequests");
        public IMongoCollection<ContactMessage> ContactMessages => _database.GetCollection<ContactMessage>("ContactMessages");
        public IMongoCollection<Foremail> Foremails => _database.GetCollection<Foremail>("Foremails");
        public IMongoCollection<SatellitePosition> SatellitePositions => _database.GetCollection<SatellitePosition>("SatellitePositions");

        public IMongoCollection<TLERockets> TLEROCKETS => _database.GetCollection<TLERockets>("TLERockets"); 
        public IMongoCollection<TLEUnknown> TLEUNKNOWN => _database.GetCollection<TLEUnknown>("TLEUnknown");
        public IMongoCollection<TLEDebris> TLEs => _database.GetCollection<TLEDebris>("TLEDebris");

        // New collection for Payloads

        ////nrmeen
        //public IMongoCollection<EciPosition> EciPositions => _database.GetCollection<EciPosition>("EciPositions");
        //public IMongoCollection<GeodeticPosition> GeodeticPositions => _database.GetCollection<GeodeticPosition>("GeodeticPositions");
        //public IMongoCollection<Satellite> Satellites => _database.GetCollection<Satellite>("Satellites");
        ////nrmeen
        //        public IMongoCollection<TLENoneActivePayloads> TLENoneActivePAYLOADS => _database.GetCollection<TLENoneActivePayloads>("TLENoneActivePayloads");

    }
}




