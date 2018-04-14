using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IEpisodeService
    {
        Task<string> GetEpisodes(int showId, bool includeSpecials);
        Task<string> GetEpisodes(int showId, int seasonId, int episodeId);
        Task<string> GetEpisodes(int showId, DateTime date);

        Task<string> GetEpisodes(DateTime date);
    }
}
