using System;
using Newtonsoft.Json;
using showTracker.Model.Annotations;

namespace showTracker.Model.API.Dto
{
    public class PeopleDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public CountryDto Country { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public string Gender { get; set; }
        [CanBeNull] public ImageDto Image { get; set; }
        [JsonProperty("_links")]
        public LinksDto Links { get; set; }
    }
}
