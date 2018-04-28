using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
using showTracker.Model.API.Dto;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.FavouritiesPage
{
    public class FavouritiesViewModel : BaseViewModel
    {
        private readonly IFavouritiesService _favouritiesService;
        private readonly ISTLogger _logger;

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

        public FavouritiesViewModel(IFavouritiesService favouritiesService, ISTLogger logger)
        {
            _favouritiesService = favouritiesService;
            _logger = logger;

            PageTitle = "List of your favourite shows";
            Shows = new List<ShowDto>();
            
            Load();

        }

        private void Load()
        {
            IsLoading = true;
            try
            {
                var shows = _favouritiesService.FavouritiesShowCollection;
                _logger.LogWithSerialization(shows);

                Shows = shows.ToList();
            }
            catch (HttpRequestException e)
            {
                _logger.Log($"Exception: {e.Message}\n\nStackTrace: {e.StackTrace}");

                PopupAlertTitle = Constants.NoInternetConnection;
                PopupAlertMessage = Constants.CheckYourInternetConnection;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }
            catch (Exception e)
            {
                _logger.Log($"Exception: {e.Message}\n\nStackTrace: {e.StackTrace}");

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
