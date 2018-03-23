using showTracker.Model.API.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace showTracker.BusinessLayer.ShowService
{
    public interface IShowService
    {
        IEnumerable<ShowDto> GetShows();
    }
}
