using System.Collections.Generic;
using Newtonsoft.Json;

namespace showTracker.Model.API.Dto
{    
    public class FullShowDto : ShowDto
    {
        private class EmbeddedShowDeatailsDto
        {
            public IEnumerable<AkaDto> Akas { get; set; }
            public IEnumerable<CastDto> Cast { get; set; }
            public IEnumerable<EpisodeDto> Episodes { get; set; }
            public IEnumerable<SeasonDto> Seasons { get; set; }
        }

        [JsonProperty("_embedded")]
        private EmbeddedShowDeatailsDto Embedded { get; set; }

        public IEnumerable<AkaDto> Akas => Embedded?.Akas;
        public IEnumerable<CastDto> Cast => Embedded?.Cast;
        public IEnumerable<EpisodeDto> Episodes => Embedded?.Episodes;
        public IEnumerable<SeasonDto> Seasons => Embedded?.Seasons;
    }
}
