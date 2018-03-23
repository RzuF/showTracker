using System;
using System.Collections.Generic;
using System.Text;
using showTracker.Model.API.Dto;

namespace showTracker.BusinessLayer.ShowService
{
    public class ShowService : IShowService
    {
        public IEnumerable<ShowDto> GetShows()
        {
            return new List<ShowDto>
            {
                new ShowDto
                {
                    Name = "One",
                    Runtime = 45
                },
                new ShowDto
                {
                    Name = "Two",
                    Runtime = 60
                }
            };
        }
    }
}
