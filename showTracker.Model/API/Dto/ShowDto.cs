using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace showTracker.Model.API.Dto
{
    public class ShowDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Genres { get; set; }        
        public int? Runtime { get; set; }
        public DateTime? Premiered { get; set; }
        [JsonProperty("rating.average")]
        public double? Rating { get; set; }

        public string Summary { get; set; }
        public int? Updated { get; set; }
        [JsonProperty("_links")]
        public LinksDto Links { get; set; }

        [JsonIgnore]
        public ShowDto Self => this;
    }
}
