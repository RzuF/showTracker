using System.Collections.ObjectModel;
using System.Linq;
using Plugin.Settings;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
using showTracker.Model.API.Dto;
using showTracker.Model.Enum;

namespace showTracker.BusinessLayer.Services
{
    public class FavouritesService: IFavouritesService
    {        
        public bool AddShow(ShowDto show)
        {
            if (FavouritiesShowCollection.Any(x => x.Id == show.Id))
            {
                return false;
            }

            FavouritiesShowCollection.Add(show);
            SaveShowCollectionToSettings();
            return true;
        }

        public bool DeleteShow(ShowDto show)
        {
            return DeleteShow(show.Id);
        }

        public bool DeleteShow(int id)
        {
            FavouritiesShowCollection.Remove(FavouritiesShowCollection.FirstOrDefault(x => x.Id == id));
            SaveShowCollectionToSettings();
            return true;
        }

        public FavouritiesAction AddOrDelete(ShowDto show)
        {
            if (AddShow(show))
            {
                return FavouritiesAction.Add;
            }

            DeleteShow(show);
            return FavouritiesAction.Delete;
        }

        private ObservableCollection<ShowDto> _favouritiesShowCollection;
        public ObservableCollection<ShowDto> FavouritiesShowCollection => _favouritiesShowCollection = _favouritiesShowCollection ?? GetShowCollectionFromSettings();
        public bool IsFavourite(int id)
        {
            return FavouritiesShowCollection.Any(x => x.Id == id);
        }

        private readonly IJsonSerializeService _jsonSerializeService;
        private readonly ISTLogger _logger;

        public FavouritesService(IJsonSerializeService jsonSerializeService, ISTLogger logger)
        {
            _jsonSerializeService = jsonSerializeService;
            _logger = logger;
        }

        public ObservableCollection<ShowDto> GetShowCollectionFromSettings()
        {
            var serializedShows = CrossSettings.Current.GetValueOrDefault(Constants.SettingsFavouritiesCollectionName, string.Empty);
            _logger.Log($"Get: {serializedShows}");
            var collection = _jsonSerializeService.TryDeserializeObject<ObservableCollection<ShowDto>>(serializedShows);
            _logger.Log($"Can deserialize: {collection.success}");

            return collection.success ? collection.obj : new ObservableCollection<ShowDto>();
        }

        private void SaveShowCollectionToSettings()
        {
            var serializedShows = _jsonSerializeService.SerializeObject(_favouritiesShowCollection);
            _logger.Log($"Save: {serializedShows}");
            CrossSettings.Current.AddOrUpdateValue(Constants.SettingsFavouritiesCollectionName, serializedShows);
        }
    }
}
