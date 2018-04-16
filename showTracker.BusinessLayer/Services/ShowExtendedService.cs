using System.Threading.Tasks;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;

namespace showTracker.BusinessLayer.Services
{
    public class ShowExtendedService : IShowExtendedService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public ShowExtendedService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<string> GetSeasons(int showId)
        {
            var response = await _httpClientWrapper.HttpClient.GetStringAsync($"{Constants.ApiUrl}shows/{showId}/seasons");

            return response;
        }

        public async Task<string> GetCast(int showId)
        {
            var response = await _httpClientWrapper.HttpClient.GetStringAsync($"{Constants.ApiUrl}shows/{showId}/cast");

            return response;
        }

        public async Task<string> GetCrew(int showId)
        {
            var response = await _httpClientWrapper.HttpClient.GetStringAsync($"{Constants.ApiUrl}shows/{showId}/crew");

            return response;
        }

        public async Task<string> GetAkas(int showId)
        {
            var response = await _httpClientWrapper.HttpClient.GetStringAsync($"{Constants.ApiUrl}shows/{showId}/akas");

            return response;
        }
    }
}
