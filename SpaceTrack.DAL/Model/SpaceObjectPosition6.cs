using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SGPdotNET.Util;
using System;

namespace SpaceTrack.DAL.Model
{
    public class SpaceObjectPosition6
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("NoradId")]
        public int NoradId { get; set; }

        [BsonElement("ObjectName")]
        public string ObjectName { get; set; }

        [BsonElement("Positions6")]
        public List<PositionRecord6> Positions6 { get; set; } = new List<PositionRecord6>();
    }

    public class PositionRecord6
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