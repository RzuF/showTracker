using showTracker.ViewModel.MainPage;
using Unity;

namespace showTracker.ViewModel
{
    public static class DependencyInjectionRegister
    {
        public static void Register(UnityContainer unityContainer)
        {
            unityContainer.RegisterInstance(typeof(MainViewModel));
        }
    }
}
