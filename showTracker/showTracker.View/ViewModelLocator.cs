using CommonServiceLocator;
using showTracker.ViewModel.CustomControls;
using showTracker.ViewModel.MainPage;
using showTracker.ViewModel.SearchPage;

namespace showTracker.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => (MainViewModel) ServiceLocator.Current.GetInstance(typeof(MainViewModel));
        public ShowConatinerViewModel ShowConatinerViewModel => (ShowConatinerViewModel) ServiceLocator.Current.GetInstance(typeof(ShowConatinerViewModel));
        public SearchViewModel SearchViewModel => (SearchViewModel) ServiceLocator.Current.GetInstance(typeof(SearchViewModel));
    }
}
