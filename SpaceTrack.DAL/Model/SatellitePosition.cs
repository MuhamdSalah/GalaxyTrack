using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SpaceTrack.DAL.Model
{
    public class SatellitePosition
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("NoradId")]
        public int NoradId { get; set; }

        [BsonElement("ObjectName")]
        public string ObjectName { get; set; }

        [BsonElement("Longitude")]
        public double Longitude { get; set; }

        [BsonElement("Latitude")]
        public double Latitude { get; set; }

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; } // Time of this position
    }
}
