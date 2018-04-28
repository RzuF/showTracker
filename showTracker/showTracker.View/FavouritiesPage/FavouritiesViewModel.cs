using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
using showTracker.Model.API.Dto;
using showTracker.Model.Enum;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.FavouritiesPage
{
    public class FavouritiesViewModel : BaseViewModel
    {
        public ICommand ButtonClick { get; }
        public string ButtonTitle => "Go to the Scheduler";

        private readonly IFavouritiesService _favouritiesService;
        private readonly ISTLogger _logger;
        private readonly INavigationService _navigationService;

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

        public FavouritiesViewModel(IFavouritiesService favouritiesService, ISTLogger logger, INavigationService navigationService)
        {
            _favouritiesService = favouritiesService;
            _logger = logger;
            _navigationService = navigationService;

            ButtonClick = new Command(FavouritiesScheduleNavigate);
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


        private void FavouritiesScheduleNavigate()
        {
            _navigationService.Navigate(ApplicationPageEnum.FavouritiesSchedulePage);
            _logger.Log($"FavScheduleButton clicked");
        }

    }
}
