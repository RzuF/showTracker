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
            try
            {
                var shows = await _apiClientService.SearchShows(SearchPhrase);
                _stLogger.LogWithSerialization(shows);

                Shows = shows.ToList();
            }
            catch (InvalidShowException e)
            {
                _stLogger.Log($"Exception: {e.Message}\n\nStackTrace: {e.StackTrace}");

                PopupAlertTitle = Constants.WrongQuery;
                PopupAlertMessage = Constants.ErrorDuringFetchingShow;
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
