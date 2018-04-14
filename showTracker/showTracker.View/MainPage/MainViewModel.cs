using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
using showTracker.Model.Enum;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.MainPage
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand OnSearchNavigateCommand { get; }
        public ICommand OnTodayNavigateCommand { get; }
        public ICommand OnFavouritiesCommand { get; }
        public ICommand OnAboutNavigateCommand { get; }

        private readonly ISTLogger _stLogger;
        private readonly INavigationService _navigationService;
        public MainViewModel(ISTLogger stLogger, INavigationService navigationService)
        {
            _stLogger = stLogger;
            _navigationService = navigationService;

            OnSearchNavigateCommand = new Command(SearchNavigate);
            OnAboutNavigateCommand= new Command(AboutNavigate);
            OnFavouritiesCommand = new Command(FavouritiesNavigate);
            OnTodayNavigateCommand = new Command(TodayNavigate);

            PageTitle = Constants.ApplicationName;
        }

        private void SearchNavigate()
        {
            _navigationService.Navigate(ApplicationPageEnum.SearchPage);
            _stLogger.Log($"SearchTile clicked");
        }

        private void TodayNavigate()
        {
            _navigationService.Navigate(ApplicationPageEnum.TodayPage);
            _stLogger.Log($"TodayTile clicked");
        }

        private void FavouritiesNavigate()
        {
            _navigationService.Navigate(ApplicationPageEnum.FavouritiesPage);
            _stLogger.Log($"FavTile clicked");
        }

        private void AboutNavigate()
        {
            _navigationService.Navigate(ApplicationPageEnum.AboutPage);
            _stLogger.Log($"AboutTile clicked");
        }
    }
}
