using System.Collections.ObjectModel;
using showTracker.Model.API.Dto;
using showTracker.Model.Enum;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IFavouritiesService
    {
        bool AddShow(ShowDto show);
        bool DeleteShow(ShowDto show);
        bool DeleteShow(int id);
        FavouritiesAction AddOrDelete(ShowDto show);
        ObservableCollection<ShowDto> FavouritiesShowCollection { get; }
        bool IsFavourite(int id);
    }
}