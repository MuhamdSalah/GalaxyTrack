using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTrack.DAL.DTOs
{
    using System.Text.Json.Serialization;

    public class SpaceTrackSatellite
    {
        [JsonPropertyName("NORAD_CAT_ID")]
        public int NoradId { get; set; }

        [JsonPropertyName("OBJECT_NAME")]
        public string ObjectName { get; set; }
    }

}
