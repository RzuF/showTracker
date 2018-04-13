using System.Collections.Generic;
using showTracker.BusinessLayer.Interfaces;
using showTracker.BusinessLayer.TileIconStrategies;
using showTracker.Model.Enum;

namespace showTracker.BusinessLayer.Resolvers
{
    public class TileIconStrategyResolver : ITileIconStrategyResolver
    {
        private readonly Dictionary<TileIconEnum, ITileIconStrategy> _dictionary = new Dictionary<TileIconEnum, ITileIconStrategy>
        {
            {TileIconEnum.Unknown, null },
            {TileIconEnum.Search, new SearchTileIconStrategy() },
            {TileIconEnum.About, new AboutTileIconStrategy() },
            {TileIconEnum.Favourities, new FavouritiesTileIconStrategy() },
            {TileIconEnum.Today, new TodayTileIconStrategy() }
        };

        public ITileIconStrategy Resolve(TileIconEnum tileIconEnum)
        {
            return _dictionary[tileIconEnum];
        }
    }
}
