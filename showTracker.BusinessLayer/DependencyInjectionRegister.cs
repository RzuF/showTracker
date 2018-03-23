using showTracker.BusinessLayer.ShowService;
using Unity;

namespace showTracker.BusinessLayer
{
    public static class DependencyInjectionRegister
    {
        public static void Register()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IShowService, ShowService.ShowService>();
        }
    }
}
