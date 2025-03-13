﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpaceTrack.DAL.Model
{
    public class TLERockets
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("NoradId")]
        public int NoradId { get; set; }

        [BsonElement("ObjectName")]
        public string? ObjectName { get; set; } // Name of the payload


        [BsonElement("FirstLine")]
        public string FirstLine { get; set; } // First line of TLE

        [BsonElement("SecondLine")]
        public string SecondLine { get; set; } // Second line of TLE

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
