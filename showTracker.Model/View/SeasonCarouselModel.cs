using System.Collections.Generic;
using showTracker.Model.API.Dto;

namespace showTracker.Model.View
{
    public class SeasonCarouselModel
    {
        public SeasonDto Season { get; set; }
        public List<EpisodeDto> Episodes { get; set; }
        public string CarouselPageTitle { get; set; }
    }
}
