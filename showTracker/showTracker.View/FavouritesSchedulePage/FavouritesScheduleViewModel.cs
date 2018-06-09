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

namespace showTracker.ViewModel.FavouritesSchedulePage
{
    public class FavouritesScheduleViewModel : BaseViewModel
    {
        private readonly IFavouritesSchedulingService _favouritiesSchedulingService;
        private readonly ISTLogger _logger;

        public FavouritesScheduleViewModel(IFavouritesSchedulingService favouritiesSchedulingService, ISTLogger logger)
        {
            _favouritiesSchedulingService = favouritiesSchedulingService;
            _logger = logger;

            OnGenerateRequested = new Command(GenerateRequested);

            PageTitle = Constants.FavouritesSchedulePageTitle;
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
                    PopupAlertTitle = Constants.InvalidDateRangeTitle;
                    PopupAlertMessage = Constants.InvalidDateRangeMessage;
                    MessagingCenter.Send(this, Constants.PopupAlertKey);

                    return;
                }

                var episodes = await _favouritiesSchedulingService.GetScheduleForFavourities(StartDate, EndDate);
                _logger.LogWithSerialization(episodes);

                Episodes = episodes.AsQueryable().OrderBy("AirDate, AirTime").ToList();
            }
            catch (IOException ioException)
            {
                NotifyAboutException(Constants.WrongQuery, Constants.ErrorDuringFetchingShow, ioException);
            }
            catch (HttpRequestException httpRequestException)
            {
                NotifyAboutException(Constants.NoInternetConnection, Constants.CheckYourInternetConnection, httpRequestException);
            }

            catch (Exception exception)
            {
                NotifyAboutException(Constants.UndefinedError, Constants.PleaseContactDeveloper, exception);
            }
            finally
            {
                IsLoading = false;
            }            
        }

        private void NotifyAboutException(string title, string message, Exception exception)
        {
            _logger.Log($"Exception: {exception.Message}\n\nStackTrace: {exception.StackTrace}");

            PopupAlertTitle = title;
            PopupAlertMessage = message;
            MessagingCenter.Send(this, Constants.PopupAlertKey);
        }
    }
}
