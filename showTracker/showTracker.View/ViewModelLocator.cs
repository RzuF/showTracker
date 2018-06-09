using CommonServiceLocator;
using showTracker.ViewModel.AboutPage;
using showTracker.ViewModel.CustomControls;
using showTracker.ViewModel.FavouritesPage;
using showTracker.ViewModel.FavouritesSchedulePage;
using showTracker.ViewModel.FavouritiesPage;
using showTracker.ViewModel.FavouritiesSchedulePage;
using showTracker.ViewModel.MainPage;
using showTracker.ViewModel.SearchPage;
using showTracker.ViewModel.ShowPage;
using showTracker.ViewModel.TodayPage;

namespace showTracker.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => (MainViewModel) ServiceLocator.Current.GetInstance(typeof(MainViewModel));
        public SearchViewModel SearchViewModel => (SearchViewModel) ServiceLocator.Current.GetInstance(typeof(SearchViewModel));
        public TodayViewModel TodayViewModel => (TodayViewModel) ServiceLocator.Current.GetInstance(typeof(TodayViewModel));
        public EntityContainerViewModel EntityContainerViewModel => (EntityContainerViewModel) ServiceLocator.Current.GetInstance(typeof(EntityContainerViewModel));
        public FavouritesScheduleViewModel FavouritiesScheduleViewModel => (FavouritesScheduleViewModel)ServiceLocator.Current.GetInstance(typeof(FavouritesScheduleViewModel));
        public FavouritesViewModel FavouritiesViewModel =>(FavouritesViewModel) ServiceLocator.Current.GetInstance(typeof(FavouritesViewModel));
        public ShowViewModel ShowViewModel => (ShowViewModel) ServiceLocator.Current.GetInstance(typeof(ShowViewModel));
        public AboutViewModel AboutViewModel => (AboutViewModel) ServiceLocator.Current.GetInstance(typeof(AboutViewModel));
    }
}
