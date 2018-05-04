using System.Threading.Tasks;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IShowService
    {
        Task<string> GetShow(int id);
        Task<string> GetFullShow(int id);
    }
}
