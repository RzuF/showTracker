using showTracker.BusinessLayer.Interfaces;
using showTracker.BusinessLayer.Services;
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
        }
    }
}
