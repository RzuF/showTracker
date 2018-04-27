using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IFavouritiesSchedulingService
    {
        Task<IEnumerable<EpisodeDto>> GetScheduleForFavourities(DateTime startDate, DateTime endDate);
    }
}