using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.TodayPage
{
    public class TodayViewModel : BaseViewModel
    {
        public ICommand OnSearchRequested { get; }
        public ICommand OnShowConatinerLoaded { get; }

        public DateTime SearchDate { get; set; }

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

        public TodayViewModel(IApiClientService apiClientService, ISTLogger stLogger)
        {
            _apiClientService = apiClientService;
            _stLogger = stLogger;

            OnSearchRequested = new Command(SearchRequested);
            OnShowConatinerLoaded = new Command(ShowContainerLoaded);

            PageTitle = "Episodes for selected date";
            Shows = new List<ShowDto>();
        }

        private async void SearchRequested()
        {
            IsLoading = true;
            var episodes = await _apiClientService.GetEpisodes(SearchDate);
            _stLogger.LogWithSerialization(episodes);

            Shows = episodes.Select(x => x.Show).ToList();            
        }

        private void ShowContainerLoaded()
        {
            IsLoading = false;
        }
    }
}
