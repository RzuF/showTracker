using CommonServiceLocator;
using showTracker.ViewModel.CustomControls;
using showTracker.ViewModel.MainPage;
using showTracker.ViewModel.SearchPage;
using showTracker.ViewModel.TodayPage;

namespace showTracker.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => (MainViewModel) ServiceLocator.Current.GetInstance(typeof(MainViewModel));
        public ShowConatinerViewModel ShowConatinerViewModel => (ShowConatinerViewModel) ServiceLocator.Current.GetInstance(typeof(ShowConatinerViewModel));
        public SearchViewModel SearchViewModel => (SearchViewModel) ServiceLocator.Current.GetInstance(typeof(SearchViewModel));
        public TodayViewModel TodayViewModel => (TodayViewModel) ServiceLocator.Current.GetInstance(typeof(TodayViewModel));
    }
}
