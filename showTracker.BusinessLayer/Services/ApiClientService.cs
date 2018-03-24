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

        public ApiClientService(IJsonSerializeService jsonSerializeService, IShowService showService)
        {
            _jsonSerializeService = jsonSerializeService;
            _showService = showService;
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
    }
}
