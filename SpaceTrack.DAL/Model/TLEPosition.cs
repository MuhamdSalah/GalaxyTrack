using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SGPdotNET.Util;
using System;

namespace SpaceTrack.DAL.Model
{
    public class TLEPosition
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("NoradId")]
        public int NoradId { get; set; }

        [BsonElement("ObjectName")]
        public string ObjectName { get; set; }

        [BsonElement("Longitude")]
        public Angle Longitude { get; set; }

        [BsonElement("Latitude")]
        public Angle Latitude { get; set; }

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
