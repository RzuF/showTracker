using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Services
{
    public class FavouritesSchedulingService : IFavouritesSchedulingService
    {
        private readonly IFavouritesService _favouritesService;
        private readonly IApiClientService _apiClientService;
        private readonly ISTLogger _logger;

        public FavouritesSchedulingService(IFavouritesService favouritesService, IApiClientService apiClientService, ISTLogger logger)
        {
            _favouritesService = favouritesService;
            _apiClientService = apiClientService;
            _logger = logger;
        }

        public async Task<IEnumerable<EpisodeDto>> GetScheduleForFavourities(DateTime startDate, DateTime endDate)
        {
            var favoutitesCollection = _favouritesService.FavouritiesShowCollection;

            var scheduleEpisodeList = new List<EpisodeDto>();
            foreach (var show in favoutitesCollection)
            {
                var episodes = (await _apiClientService.GetEpisodes(show.Id)).Where(
                    x => x.AirDate.HasValue && x.AirDate.Value.Date >= startDate.Date && x.AirDate.Value.Date <= endDate.Date).ToList();
                foreach (var episode in episodes)
                {
                    episode.Show = show;
                }

                _logger.LogWithSerialization(episodes);

                scheduleEpisodeList.AddRange(episodes);
            }

            return scheduleEpisodeList;
        }
    }
}
