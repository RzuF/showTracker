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
        public ICommand OnEpisodeConatinerLoaded { get; }
        public ICommand OnEpisodeConatinerLoadingStarted { get; }

        public DateTime SearchDate { get; set; }

        private bool _isLoading = true;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private List<EpisodeDto> _episodes;
        public List<EpisodeDto> Episodes
        {
            get => _episodes;
            set
            {
                _episodes = value;
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
            OnEpisodeConatinerLoaded = new Command(EpisodeContainerLoaded);
            OnEpisodeConatinerLoadingStarted = new Command(EpisodeContainerLoadingStarted);

            PageTitle = "Episodes for selected date";
            Episodes = new List<EpisodeDto>();
        }

        private async void SearchRequested()
        {
            IsLoading = true;
            var episodes = await _apiClientService.GetEpisodes(SearchDate);
            _stLogger.LogWithSerialization(episodes);

            Episodes = episodes.ToList();            
        }

        private void EpisodeContainerLoaded()
        {
            IsLoading = false;
        }

        private void EpisodeContainerLoadingStarted()
        {
            IsLoading = true;
        }
    }
}
