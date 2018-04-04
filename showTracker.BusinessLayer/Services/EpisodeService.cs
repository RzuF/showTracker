using System;
using System.Threading.Tasks;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;

namespace showTracker.BusinessLayer.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public EpisodeService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<string> GetEpisodes(int showId, bool includeSpecials = true)
        {
            var specials = includeSpecials ? 1 : 0;
            var response = await _httpClientWrapper.HttpClient.GetStringAsync($"{Constants.ApiUrl}shows/{showId}/episodes?specials={specials}");

            return response;
        }

        public async Task<string> GetEpisodes(int showId, DateTime date)
        {
            var iso8601Date = date.ToString("yyyy-MM-dd");
            var response = await _httpClientWrapper.HttpClient.GetStringAsync($"{Constants.ApiUrl}shows/{showId}/episodesbydate?date={iso8601Date}");

            return response;
        }

        public async Task<string> GetEpisodes(int showId, int seasonId, int episodeId)
        {
            var response = await _httpClientWrapper.HttpClient.GetStringAsync($"{Constants.ApiUrl}shows/{showId}/episodebynumber?season={seasonId}&number={episodeId}");

            return response;
        }
    }
}
