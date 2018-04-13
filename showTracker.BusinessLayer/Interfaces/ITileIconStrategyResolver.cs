using showTracker.Model.Enum;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface ITileIconStrategyResolver
    {
        ITileIconStrategy Resolve(TileIconEnum tileIconEnum);
    }
}
