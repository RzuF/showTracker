using System;
using System.Collections.Generic;
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
        private readonly IShowExtendedService _showExtendedService;

        public ApiClientService(IJsonSerializeService jsonSerializeService, IShowService showService, IEpisodeService episodeService, ISearchService searchService, IShowExtendedService showExtendedService)
        {
            _jsonSerializeService = jsonSerializeService;
            _showService = showService;
            _episodeService = episodeService;
            _searchService = searchService;
            _showExtendedService = showExtendedService;
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
                var shows = _jsonSerializeService.TryDeserializeObject<IEnumerable<ShowDto>>(json);

                if (shows.success)
                {
                    return shows.obj;
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

        public async Task<IEnumerable<SeasonDto>> GetSeasons(int showId)
        {
            var json = await _showExtendedService.GetSeasons(showId);
            var seasons = _jsonSerializeService.TryDeserializeObject<IEnumerable<SeasonDto>>(json);

            if (seasons.success)
            {
                return seasons.obj;
            }

            throw new InvalidSeasonException($"Get season for showId: {showId}");
        }

        public async Task<IEnumerable<CastDto>> GetCast(int showId)
        {
            var json = await _showExtendedService.GetCast(showId);
            var cast = _jsonSerializeService.TryDeserializeObject<IEnumerable<CastDto>>(json);

            if (cast.success)
            {
                return cast.obj;
            }

            throw new Exceptions.InvalidCastException($"Get cast for showId: {showId}");
        }

        public async Task<IEnumerable<CrewDto>> GetCrew(int showId)
        {
            var json = await _showExtendedService.GetCrew(showId);
            var crew = _jsonSerializeService.TryDeserializeObject<IEnumerable<CrewDto>>(json);

            if (crew.success)
            {
                return crew.obj;
            }

            throw new InvalidCrewException($"Get crew for showId: {showId}");
        }

        public async Task<IEnumerable<AkaDto>> GetAkas(int showId)
        {
            var json = await _showExtendedService.GetAkas(showId);
            var akas = _jsonSerializeService.TryDeserializeObject<IEnumerable<AkaDto>>(json);

            if (akas.success)
            {
                return akas.obj;
            }

            throw new InvalidAkaException($"Get show name alias' for showId: {showId}");
        }
    }
}
