using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
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

            PageTitle = "Schedule of your shows";
            Episodes = new List<EpisodeDto>();
        }

        public ICommand OnGenerateRequested { get; }

        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(7);

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
            try
            {
                if (StartDate > EndDate)
                {
                    PopupAlertTitle = "Invalid date range";
                    PopupAlertMessage = "Selected date range cannot start after end date!";
                    MessagingCenter.Send(this, Constants.PopupAlertKey);

                    return;
                }

                var episodes = await _favouritiesSchedulingService.GetScheduleForFavourities(StartDate, EndDate);
                _logger.LogWithSerialization(episodes);

                Episodes = episodes.AsQueryable().OrderBy("AirDate, AirTime").ToList();
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.Log($"Exception: {httpRequestException.Message}\n\nStackTrace: {httpRequestException.StackTrace}");

                PopupAlertTitle = Constants.NoInternetConnection;
                PopupAlertMessage = Constants.CheckYourInternetConnection;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }
            catch (IOException ioException)
            {
                _logger.Log($"Exception: {ioException.Message}\n\nStackTrace: {ioException.StackTrace}");

                PopupAlertTitle = Constants.NoInternetConnection;
                PopupAlertMessage = Constants.CheckYourInternetConnection;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }

            catch (Exception exception)
            {
                _logger.Log($"Exception: {exception.Message}\n\nStackTrace: {exception.StackTrace}");

                PopupAlertTitle = Constants.UndefinedError;
                PopupAlertMessage = Constants.PleaseContactDeveloper;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }
            finally
            {
                IsLoading = false;
            }                       
        }
    }
}
