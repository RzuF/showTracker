using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;

namespace showTracker.BusinessLayer.TileIconStrategies
{
    public class TodayTileIconStrategy : ITileIconStrategy
    {
        public string ResourceId { get; } = Constants.TodayIconResourceId;
        public string Label { get; } = Constants.TodayTileLabel;
    }
}
