using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;

namespace showTracker.BusinessLayer.TileIconStrategies
{
    public class SearchTileIconStrategy : ITileIconStrategy
    {
        public string ResourceId { get; } = Constants.SearchIconResourceId;
        public string Label { get; } = Constants.SearchTileLabel;
    }
}
