using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using Xamarin.Forms;

namespace showTracker.ViewModel.MainPage
{
    public class MainViewModel
    {
        public ICommand OnSearchNavigateCommand { get; }
        public ICommand OnTodayNavigateCommand { get; }
        public ICommand OnFavouritiesCommand { get; }
        public ICommand OnAboutNavigateCommand { get; }

        private readonly ISTLogger _stLogger;
        public MainViewModel(ISTLogger stLogger)
        {
            _stLogger = stLogger;
            OnSearchNavigateCommand = new Command(SearchNavigate);
            OnAboutNavigateCommand= new Command(AboutNavigate);
            OnFavouritiesCommand = new Command(FavouritiesNavigate);
            OnTodayNavigateCommand = new Command(TodayNavigate);
        }

        private void SearchNavigate()
        {
            //Application.Current.MainPage.Navigation.PushAsync(new showTracker.MainPage())
            _stLogger.Log($"SearchTile clicked");
        }

        private void TodayNavigate()
        {
            _stLogger.Log($"TodayTile clicked");
        }

        private void FavouritiesNavigate()
        {
            _stLogger.Log($"FavTile clicked");
        }

        private void AboutNavigate()
        {
            _stLogger.Log($"AboutTile clicked");
        }
    }
}
