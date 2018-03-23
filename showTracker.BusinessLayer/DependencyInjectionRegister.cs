using showTracker.BusinessLayer.ShowService;
using Unity;

namespace showTracker.BusinessLayer
{
    public static class DependencyInjectionRegister
    {
        public static void Register(UnityContainer unityContainer)
        {            
            unityContainer.RegisterType<IShowService, ShowService.ShowService>();
        }
    }
}
