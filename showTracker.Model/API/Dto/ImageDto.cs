﻿using Newtonsoft.Json;

namespace showTracker.Model.API.Dto
{
    public class ImageDto
    {
        [JsonProperty("medium")]
        public string MediumImgUrl { get; set; }

        [JsonProperty("original")]
        public string OriginalImgUrl { get; set; }
    }
}
