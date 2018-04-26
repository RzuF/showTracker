using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.FavouritiesSchedulePage
{
    public class FavouritiesScheduleViewModel : BaseViewModel
    {
        private readonly IFavouritiesSchedulingService _favouritiesSchedulingService;
        private readonly ISTLogger _logger;

        public FavouritiesScheduleViewModel(IFavouritiesSchedulingService favouritiesSchedulingService, ISTLogger logger)
        {
            _favouritiesSchedulingService = favouritiesSchedulingService;
            _logger = logger;

            OnGenerateRequested = new Command(GenerateRequested);

            PageTitle = "Schedule of your favourities shows";
            Episodes = new List<EpisodeDto>();
        }

        public ICommand OnGenerateRequested { get; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

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

        private async void GenerateRequested()
        {
            IsLoading = true;
            var episodes = await _favouritiesSchedulingService.GetScheduleForFavourities(StartDate, EndDate);
            _logger.LogWithSerialization(episodes);

            Episodes = episodes.ToList();
            IsLoading = false;
        }
    }
}
