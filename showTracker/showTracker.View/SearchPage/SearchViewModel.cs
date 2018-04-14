using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.SearchPage
{
    public class SearchViewModel : BaseViewModel
    {
        public ICommand OnSearchRequested { get; }

        public string SearchPhrase { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private List<ShowDto> _shows;
        public List<ShowDto> Shows
        {
            get => _shows;
            set
            {
                _shows = value;
                OnPropertyChanged();
            }
        }

        private readonly ISTLogger _stLogger;
        private readonly IApiClientService _apiClientService;

        public SearchViewModel(IApiClientService apiClientService, ISTLogger stLogger)
        {
            _apiClientService = apiClientService;
            _stLogger = stLogger;

            OnSearchRequested = new Command(SearchRequested);

            PageTitle = "Search for shows";
            Shows = new List<ShowDto>();
        }

        private async void SearchRequested()
        {
            IsLoading = true;
            var shows = await _apiClientService.SearchShows(SearchPhrase);
            _stLogger.LogWithSerialization(shows);

            Shows = shows.ToList();
            IsLoading = false;
        }
    }
}
