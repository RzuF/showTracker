using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IEpisodeService
    {
        Task<string> GetEpisodes(int showId, bool includeSpecials);
        Task<string> GetEpisodesByNumber(int showId, int seasonId, int episodeId);
        Task<string> GetEpisodesByDate(int showId, DateTime date);

    }
}
