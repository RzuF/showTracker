using Newtonsoft.Json;

namespace showTracker.Model.API.Dto
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public ImageDto Image { get; set; }
        [JsonProperty("_links")]
        public LinksDto Links { get; set; }
    }
}
