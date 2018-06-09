using showTracker.ViewModel.AboutPage;
using showTracker.ViewModel.CustomControls;
using showTracker.ViewModel.FavouritiesSchedulePage;
using showTracker.ViewModel.MainPage;
using showTracker.ViewModel.SearchPage;
using showTracker.ViewModel.ShowPage;
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
            unityContainer.RegisterInstance(typeof(FavouritesScheduleViewModel));
            unityContainer.RegisterInstance(typeof(ShowViewModel));
            unityContainer.RegisterInstance(typeof(AboutViewModel));

            unityContainer.RegisterInstance(typeof(MainPage.MainPage));
            unityContainer.RegisterInstance(typeof(AboutPage.AboutPage));
            unityContainer.RegisterInstance(typeof(FavouritiesPage.FavouritiesPage));
            unityContainer.RegisterInstance(typeof(SearchPage.SearchPage));
            unityContainer.RegisterInstance(typeof(TodayPage.TodayPage));
            unityContainer.RegisterInstance(typeof(FavouritiesSchedulePage.FavouritiesSchedulePage));
            unityContainer.RegisterInstance(typeof(ShowPage.ShowPage));
        }
    }
}
