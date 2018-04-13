using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;

namespace showTracker.BusinessLayer.TileIconStrategies
{
    public class FavouritiesTileIconStrategy : ITileIconStrategy
    {
        public string ResourceId { get; } = Constants.FavouriteIconResourceId;
        public string Label { get; } = Constants.FavouritiesTileLabel;
    }
}
