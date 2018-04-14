using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showTracker.BusinessLayer.Exceptions;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly IJsonSerializeService _jsonSerializeService;
        private readonly IShowService _showService;
        private readonly IEpisodeService _episodeService;
        private readonly ISearchService _searchService;

        public ApiClientService(IJsonSerializeService jsonSerializeService, IShowService showService, IEpisodeService episodeService, ISearchService searchService)
        {
            _jsonSerializeService = jsonSerializeService;
            _showService = showService;
            _episodeService = episodeService;
            _searchService = searchService;
        }

        public async Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, bool includeSpecials = true)
        {
            var json = await _episodeService.GetEpisodes(showId, includeSpecials);
            var episodes = _jsonSerializeService.TryDeserializeObject<IEnumerable<EpisodeDto>>(json);
            if (episodes.success)
            {
                return episodes.obj;
            }

            throw new InvalidEpisodeException($"Show id: {showId}; Include specials: {includeSpecials}");
        }

        public async Task<EpisodeDto> GetEpisode(int showId, int seasonId, int episodeId)
        {
            var json = await _episodeService.GetEpisodes(showId, seasonId, episodeId);
            var episodes = _jsonSerializeService.TryDeserializeObject<EpisodeDto>(json);
            if (episodes.success)
            {
                return episodes.obj;
            }

            throw new InvalidEpisodeException($"Show id: {showId}; Season id: {seasonId}; Episode id: {episodeId}");
        }

        public async Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, int seasonId, int episodeId)
        {
            var episode = await GetEpisode(showId, seasonId, episodeId);
            return new List<EpisodeDto>{episode};
        }

        public async Task<IEnumerable<EpisodeDto>> GetEpisodes(int showId, DateTime date)
        {
            var json = await _episodeService.GetEpisodes(showId, date);
            var episodes = _jsonSerializeService.TryDeserializeObject<IEnumerable<EpisodeDto>>(json);
            if (episodes.success)
            {
                return episodes.obj;
            }

            throw new InvalidEpisodeException($"Show id: {showId}; Date: {date.ToString($"yyyy-MM-dd")}");
        }

        public async Task<ShowDto> GetShow(int id)
        {
            var json = await _showService.GetShow(id);
            var show = _jsonSerializeService.TryDeserializeObject<ShowDto>(json);

            if (show.success)
            {
                return show.obj;
            }

            throw new InvalidShowException($"Show id: {id}");
        }

        public async Task<ShowDto> SearchShow(string query)
        {
            var json = await _searchService.SearchShows(query, true);
            var show = _jsonSerializeService.TryDeserializeObject<ShowDto>(json);

            if (show.success)
            {
                return show.obj;
            }

            throw new InvalidShowException($"Show search query: {query}, singlesearch");
        }

        public async Task<IEnumerable<ShowDto>> SearchShows(string query, bool singlesearch = false)
        {
            if (singlesearch)
            {
                var show = await SearchShow(query);
                return new List < ShowDto >{ show };
            }
            else
            {
                var json = await _searchService.SearchShows(query);
                var shows = _jsonSerializeService.TryDeserializeObject<IEnumerable<SearchShowResultDto>>(json);

                if (shows.success)
                {
                    return shows.obj.Select(x => x.Show);
                }

                throw new InvalidShowException($"Show search query: {query}, search");
            }
        }

        public async Task<IEnumerable<PeopleDto>> SearchPeople(string query)
        {
            var json = await _searchService.SearchPeople(query);
            var people = _jsonSerializeService.TryDeserializeObject<IEnumerable<PeopleDto>>(json);

            if (people.success)
            {
                return people.obj;
            }

            throw new InvalidPersonException($"Person search query: {query}");
        }
    }
}
