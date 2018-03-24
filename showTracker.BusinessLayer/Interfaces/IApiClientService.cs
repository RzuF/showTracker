using System.Threading.Tasks;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IApiClientService
    {
        Task<ShowDto> GetShow(int id);
    }
}
