using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface ISearchService
    {
        Task<string> SearchShows(string query, bool singlesearch = false);
        Task<string> SearchPeople(string query);
    }
}
