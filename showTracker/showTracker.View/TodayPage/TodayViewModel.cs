using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using showTracker.BusinessLayer.Exceptions;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
using showTracker.Model.API.Dto;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.TodayPage
{
    public class TodayViewModel : BaseViewModel
    {
        public ICommand OnSearchRequested { get; }

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

            PageTitle = "Episodes for selected date";
            Episodes = new List<EpisodeDto>();
        }

        private async void SearchRequested()
        {
            IsLoading = true;
            try
            {
                var episodes = await _apiClientService.GetEpisodes(SearchDate);
                _stLogger.LogWithSerialization(episodes);

                Episodes = episodes.ToList();
            }
            catch (InvalidEpisodeException e)
            {
                _stLogger.Log($"Exception: {e.Message}\n\nStackTrace: {e.StackTrace}");

                PopupAlertTitle = Constants.WrongQuery;
                PopupAlertMessage = Constants.ErrorDuringFetchingEpisode;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }
            catch (HttpRequestException e)
            {
                _stLogger.Log($"Exception: {e.Message}\n\nStackTrace: {e.StackTrace}");

                PopupAlertTitle = Constants.NoInternetConnection;
                PopupAlertMessage = Constants.CheckYourInternetConnection;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }

            catch (Exception e)
            {
                _stLogger.Log($"Exception: {e.Message}\n\nStackTrace: {e.StackTrace}");

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
