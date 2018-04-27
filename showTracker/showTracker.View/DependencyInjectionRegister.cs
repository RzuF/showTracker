using showTracker.ViewModel.CustomControls;
using showTracker.ViewModel.FavouritiesSchedulePage;
using showTracker.ViewModel.MainPage;
using showTracker.ViewModel.SearchPage;
using showTracker.ViewModel.TodayPage;
using Unity;

namespace showTracker.ViewModel
{
    public static class DependencyInjectionRegister
    {
        public static void Register(UnityContainer unityContainer)
        {
            unityContainer.RegisterInstance(typeof(MainViewModel));
            unityContainer.RegisterInstance(typeof(SearchViewModel));
            unityContainer.RegisterInstance(typeof(TodayViewModel));
            unityContainer.RegisterInstance(typeof(EntityContainerViewModel));
            unityContainer.RegisterInstance(typeof(FavouritiesScheduleViewModel));

            unityContainer.RegisterInstance(typeof(MainPage.MainPage));
            unityContainer.RegisterInstance(typeof(AboutPage.AboutPage));
            unityContainer.RegisterInstance(typeof(FavouritiesPage.FavouritiesPage));
            unityContainer.RegisterInstance(typeof(SearchPage.SearchPage));
            unityContainer.RegisterInstance(typeof(TodayPage.TodayPage));
            unityContainer.RegisterInstance(typeof(FavouritiesSchedulePage.FavouritiesSchedulePage));
        }
    }
}
