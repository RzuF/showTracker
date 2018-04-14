using System.Threading.Tasks;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IShowExtendedService
    {
        Task<string> GetSeasons(int showId);
        Task<string> GetCast(int showId);
        Task<string> GetCrew(int showId);
        Task<string> GetAkas(int showId);
    }
}
