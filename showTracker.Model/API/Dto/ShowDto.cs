using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace showTracker.Model.API.Dto
{
    public class ShowDto
    {
        private class EmbeddedRatingDto
        {
            public double? Average { get; set; }
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Genres { get; set; }        
        public int? Runtime { get; set; }
        public DateTime? Premiered { get; set; }
        [JsonIgnore]
        public DateTime PremieredNotNull => Premiered ?? DateTime.MinValue;
        [JsonProperty("rating")]
        private EmbeddedRatingDto EmbeddedRating { get; set; }

        [JsonIgnore]
        public double? Rating => EmbeddedRating?.Average;

        public string Summary { get; set; }
        public int? Updated { get; set; }
        [JsonProperty("_links")]
        public LinksDto Links { get; set; }

        public ImageDto Image { get; set; }

        [JsonIgnore]
        public ShowDto Self => this;
    }
}
