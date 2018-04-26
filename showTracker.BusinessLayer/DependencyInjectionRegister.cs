using showTracker.BusinessLayer.Interfaces;
using showTracker.BusinessLayer.Loggers;
using showTracker.BusinessLayer.Resolvers;
using showTracker.BusinessLayer.Services;
using showTracker.BusinessLayer.Wrappers;
using Unity;

namespace showTracker.BusinessLayer
{
    public static class DependencyInjectionRegister
    {
        public static void Register(UnityContainer unityContainer)
        {
            unityContainer.RegisterType<IJsonSerializeService, JsonSerializeService>();
            unityContainer.RegisterType<IShowService, ShowService>();
            unityContainer.RegisterType<ISearchService, SearchService>();
            unityContainer.RegisterType<IEpisodeService, EpisodeService>();
            unityContainer.RegisterType<IApiClientService, ApiClientService>();
            unityContainer.RegisterType<IHttpClientWrapper, HttpClientWrapper>();            
            unityContainer.RegisterType<IEpisodeService, EpisodeService>();
            unityContainer.RegisterType<ITileIconStrategyResolver, TileIconStrategyResolver>();
            unityContainer.RegisterType<IShowExtendedService, ShowExtendedService>();
            unityContainer.RegisterType<IFavouritiesSchedulingService, FavouritiesSchedulingService>();
            unityContainer.RegisterSingleton<IFavouritiesService, FavouritiesService>();
            unityContainer.RegisterSingleton<INavigationService, NavigationService>();

#if DEBUG
            unityContainer.RegisterType<ISTLogger, STLogger>();
#else
            unityContainer.RegisterType<ISTLogger, ReleaseLogger>();
#endif
        }
    }
}
