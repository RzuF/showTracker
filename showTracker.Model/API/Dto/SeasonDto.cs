using System;
using Newtonsoft.Json;

namespace showTracker.Model.API.Dto
{
    public class SeasonDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? Number { get; set; }
        public string Name { get; set; }
        public int? EpisodeOrder { get; set; }
        public DateTime? PremiereDate { get; set; }
        public DateTime? EndDate { get; set; }
        public NetworkDto Network { get; set; }
        public string WebChannel { get; set; }
        public ImageDto Image { get; set; }
        public string Summary { get; set; }
        [JsonProperty("_links")]
        public LinksDto Links { get; set; }
    }
}
