using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;

namespace showTracker.BusinessLayer.TileIconStrategies
{
    public class AboutTileIconStrategy : ITileIconStrategy
    {
        public string ResourceId { get; } = Constants.AboutIconResourceId;
        public string Label { get; } = Constants.AboutTileLabel;
    }
}
