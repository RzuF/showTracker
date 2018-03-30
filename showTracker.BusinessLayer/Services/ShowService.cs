using System.Threading.Tasks;
using showTracker.BusinessLayer.Interfaces;

namespace showTracker.BusinessLayer.Services
{
    public class ShowService : IShowService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public ShowService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<string> GetShow(int id)
        {
            var response = await _httpClientWrapper.HttpClient.GetStringAsync($"{Model.Constants.ApiUrl}shows/{id}");

            return response;
        }
    }
}
