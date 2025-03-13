///////////////////////////////////////////////////////////////////////////////////////////////////worka
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
//using SGPdotNET.Util;
//using System;

//namespace SpaceTrack.DAL.Model
//{
//    public class SpaceObjectPosition
//    {
//        [BsonId]
//        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
//        public string Id { get; set; }

//        [BsonElement("NoradId")]
//        public int NoradId { get; set; }

//        [BsonElement("ObjectName")]
//        public string ObjectName { get; set; }

//        [BsonElement("Timestamp")]
//        public DateTime Timestamp { get; set; }

//        [BsonElement("Latitude")]
//        public Double Latitude { get; set; }

//        [BsonElement("Longitude")]
//        public Double Longitude { get; set; }

//        [BsonElement("Altitude")]
//        public double Altitude { get; set; }
//    }
//}
///////////////////////////////////////////////////////////////////////////////////////////////////worka
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SGPdotNET.Util;
using System;

namespace SpaceTrack.DAL.Model
{
    public class SpaceObjectPosition
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("NoradId")]
        public int NoradId { get; set; }

        [BsonElement("ObjectName")]
        public string ObjectName { get; set; }

        [BsonElement("Positions")]
        public List<PositionRecord> Positions { get; set; } = new List<PositionRecord>();
    }

    public class PositionRecord
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
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
//using System;
//using System.Collections.Generic;

//namespace SpaceTrack.DAL.Model
//{
//    public class SpaceObjectPosition
//    {
//        [BsonId]
//        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
//        public string Id { get; set; }

//        [BsonElement("NoradId")]
//        public int NoradId { get; set; }

//        [BsonElement("ObjectName")]
//        public string ObjectName { get; set; }

//        [BsonElement("Positions")]
//        public List<PositionData> Positions { get; set; }
//    }

//    public class PositionData
//    {
//        [BsonElement("Longitude")]
//        public double Longitude { get; set; }

//        [BsonElement("Latitude")]
//        public double Latitude { get; set; }

//        [BsonElement("Time")]
//        public DateTime Time { get; set; }
//    }
//}

