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

        public FavouritiesSchedulingService(IFavouritiesService favouritiesService, IApiClientService apiClientService)
        {
            _favouritiesService = favouritiesService;
            _apiClientService = apiClientService;
        }

        public async Task<IEnumerable<EpisodeDto>> GetScheduleForFavourities(DateTime startDate, DateTime endDate)
        {
            var favoutitiesCollection = _favouritiesService.FavouritiesShowCollection;

            var scheduleEpisodeList = new List<EpisodeDto>();
            foreach (var show in favoutitiesCollection)
            {
                var episodes = (await _apiClientService.GetEpisodes(show.Id)).Where(
                    x => x.AirDate.Date >= startDate.Date && x.AirDate.Date <= endDate.Date);

                scheduleEpisodeList.AddRange(episodes);
            }

            return scheduleEpisodeList;
        }
    }
}
