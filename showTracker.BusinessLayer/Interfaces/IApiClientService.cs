using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IApiClientService
    {
        Task<ShowDto> GetShow(int id);
        Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, bool includeSpecials);
        Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, int seasonId, int episodeId);
        Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, DateTime date);
    }
}
