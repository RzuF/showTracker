using System.Threading.Tasks;
using showTracker.BusinessLayer.Interfaces;
using Constants = showTracker.Model.Constants;

namespace showTracker.BusinessLayer.Services
{
    public class SearchService : ISearchService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public SearchService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<string> SearchShows(string query, bool singlesearch = false)
        {
            var searchKeyword = singlesearch ? "singlesearch" : "search";
            var response =
                await _httpClientWrapper.HttpClient.GetStringAsync(
                    $"{Constants.ApiUrl}{searchKeyword}/shows?q={query}");
            return response;
        }

        public async Task<string> SearchPeople(string query)
        {
            var response =
                await _httpClientWrapper.HttpClient.GetStringAsync($"{Constants.ApiUrl}search/people?q={query}");
            return response;
        }
    }
}
