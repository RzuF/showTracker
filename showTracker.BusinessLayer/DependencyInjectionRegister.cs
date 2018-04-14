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
            unityContainer.RegisterType<IApiClientService, ApiClientService>();
            unityContainer.RegisterType<IHttpClientWrapper, HttpClientWrapper>();
            unityContainer.RegisterType<ISTLogger, STLogger>();
            unityContainer.RegisterType<IEpisodeService, EpisodeService>();
            unityContainer.RegisterType<ITileIconStrategyResolver, TileIconStrategyResolver>();
            unityContainer.RegisterSingleton<INavigationService, NavigationService>();
        }
    }
}
