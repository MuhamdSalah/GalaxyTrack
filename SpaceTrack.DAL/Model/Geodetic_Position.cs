using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SpaceTrack.DAL.Model
{
    public class Geodetic_Position
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("NoradId")]
        public int NoradId { get; set; }

        [BsonElement("UtcTime")]
        public DateTime UtcTime { get; set; }

        [BsonElement("Latitude")]
        public double Latitude { get; set; }

        [BsonElement("Longitude")]
        public double Longitude { get; set; }

        [BsonElement("Altitude")]
        public double Altitude { get; set; }
    }
}
