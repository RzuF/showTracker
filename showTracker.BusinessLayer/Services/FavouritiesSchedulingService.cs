using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Services
{
    public class FavouritiesSchedulingService : IFavouritiesSchedulingService
    {
        private readonly IFavouritiesService _favouritiesService;
        private readonly IApiClientService _apiClientService;
        private readonly ISTLogger _logger;

        public FavouritiesSchedulingService(IFavouritiesService favouritiesService, IApiClientService apiClientService, ISTLogger logger)
        {
            _favouritiesService = favouritiesService;
            _apiClientService = apiClientService;
            _logger = logger;
        }

        public async Task<IEnumerable<EpisodeDto>> GetScheduleForFavourities(DateTime startDate, DateTime endDate)
        {
            var favoutitiesCollection = _favouritiesService.FavouritiesShowCollection;

            var scheduleEpisodeList = new List<EpisodeDto>();
            foreach (var show in favoutitiesCollection)
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
