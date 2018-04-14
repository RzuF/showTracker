using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IApiClientService
    {
        Task<ShowDto> GetShow(int id);
        Task<EpisodeDto> GetEpisode(int showId, int seasonId, int episodeId);
        Task<ShowDto> SearchShow(string query);
        Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, bool includeSpecials = true);
        Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, int seasonId, int episodeId);
        Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, DateTime date);
        Task<IEnumerable<ShowDto>> SearchShows(string query, bool singlesearch = false);
        Task<IEnumerable<PeopleDto>> SearchPeople(string query);
        Task<IEnumerable<SeasonDto>> GetSeasons(int showId);
        Task<IEnumerable<CastDto>> GetCast(int showId);
        Task<IEnumerable<CrewDto>> GetCrew(int showId);
        Task<IEnumerable<AkaDto>> GetAkas(int showId);
    }
}
