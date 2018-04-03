using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace showTracker.Model.API.Dto
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public DateTime AirDate { get; set; }
        public DateTime AirTime { get; set; }
        public DateTime AirStamp { get; set; }
        public int Runtime { get; set; }
        public ImageDto Image { get; set; }
        public string Summary { get; set; }
        [JsonProperty("_links")]
        public LinksDto Links { get; set; }
    }
}
