using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SGPdotNET.Util;
using System;

namespace SpaceTrack.DAL.Model
{
    public class SpaceObjectPosition8
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("NoradId")]
        public int NoradId { get; set; }

        [BsonElement("ObjectName")]
        public string ObjectName { get; set; }

        [BsonElement("Positions8")]
        public List<PositionRecord8> Positions8 { get; set; } = new List<PositionRecord8>();
    }

    public class PositionRecord8
    {
        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("Latitude")]
        public double Latitude { get; set; }

        [BsonElement("Longitude")]
        public double Longitude { get; set; }

        [BsonElement("Altitude")]
        public double Altitude { get; set; }
    }
}