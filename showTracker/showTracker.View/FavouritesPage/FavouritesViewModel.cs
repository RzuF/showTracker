﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
using showTracker.Model.API.Dto;
using showTracker.Model.Enum;
using showTracker.Model.Filters;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.FavouritesPage
{
    public class FavouritesViewModel : BaseViewModel
    {
        public ICommand ButtonClick { get; }
        public ICommand OnFiltersChanged { get; }
        public ICommand OnFiltersToggle { get; }
        public ICommand RefreshShows { get; }
        public string ButtonTitle => Constants.GoToScheduler;

        private readonly IFavouritesService _favouritesService;
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
                ApplyFilters();
                OnPropertyChanged();
            }
        }

        private List<ShowDto> _filteredShows;
        public List<ShowDto> FilteredShows
        {
            get => _filteredShows;
            set
            {
                _filteredShows = value;
                OnPropertyChanged();
            }
        }

        private Filters _filters;
        public Filters Filters
        {
            get => _filters;
            set
            {
                _filters = value;
                OnPropertyChanged();
            }
        }

        public Filters DefaultFilters { get; }

        private bool _isFilterEditorVisible;
        public bool IsFilterEditorVisible
        {
            get => _isFilterEditorVisible;
            set
            {
                _isFilterEditorVisible = value;
                OnPropertyChanged();
            }
        }

        private string _groupBy;
        public string GroupBy
        {
            get => _groupBy;
            set
            {
                _groupBy = value;
                OnPropertyChanged();
            }
        }

        public FavouritesViewModel(IFavouritesService favouritesService, ISTLogger logger, INavigationService navigationService)
        {
            _favouritesService = favouritesService;
            _logger = logger;
            _navigationService = navigationService;

            ButtonClick = new Command(FavouritiesScheduleNavigate);
            OnFiltersChanged = new Command(ApplyFilters);
            OnFiltersToggle = new Command(FiltersToggle);
            RefreshShows = new Command(Refresh);

            Filters = GenerateDefaultFilters();
            DefaultFilters = GenerateDefaultFilters();

            PageTitle = Constants.FavouritesPageTitle;
            Shows = new List<ShowDto>();

            Task.Run(() =>
            {
                Load();
            });
        }

        private void ApplyFilters()
        {
            IsFilterEditorVisible = false;

            FilteredShows = Shows.ToList();

            FilteredShows = FilteredShows.Where(x =>
                    (!x.Rating.HasValue || x.Rating.Value >= Filters.MinRating) &&
                    (!x.Runtime.HasValue || x.Runtime.Value >= Filters.MinRuntime))
                .ToList();

            if (Filters.Genre != "")
            {
                FilteredShows = FilteredShows.Where(x => x.Genres.Select(y => y.ToLower()).Contains(Filters.Genre.Trim().ToLower())).ToList();
            }

            if (Filters.Status != StatusEnum.None)
            {
                FilteredShows = FilteredShows.Where(x => x.Status == Enum.GetName(typeof(StatusEnum), Filters.Status)).ToList();
            }

            switch (Filters.GroupBy)
            {
                case GroupByEnum.None:
                    GroupBy = null;
                    break;
                case GroupByEnum.Type:
                    GroupBy = "Type";
                    break;
                case GroupByEnum.Status:
                    GroupBy = "Status";
                    break;
                case GroupByEnum.PremieredYear:
                    GroupBy = "PremieredNotNull.Year";
                    break;
                case GroupByEnum.Runtime:
                    GroupBy = "Runtime";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Filters.OrderBy != OrderByEnum.None)
            {
                var orderByString =
                    $"{(GroupBy == null ? "" : GroupBy + ",")} {Enum.GetName(typeof(OrderByEnum), Filters.OrderBy)} {(Filters.IsOrderByAscending ? "asc" : "desc")}"
                        .Replace(" Year", " PremieredNotNull.Year");
                FilteredShows = FilteredShows.AsQueryable()
                    .OrderBy(orderByString).ToList();
            }
        }

        private void FiltersToggle()
        {
            IsFilterEditorVisible = !IsFilterEditorVisible;
        }

        private void Load()
        {
            IsLoading = true;
            try
            {
                var shows = _favouritesService.FavouritiesShowCollection;
                _logger.LogWithSerialization(shows);

                Shows = shows.ToList();
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

        private void Refresh()
        {
            IsLoading = true;
            try
            {
                var shows = _favouritesService.GetShowCollectionFromSettings();
                _logger.LogWithSerialization(shows);

                Shows = shows.ToList();
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

        private void FavouritiesScheduleNavigate()
        {
            _navigationService.Navigate(ApplicationPageEnum.FavouritiesSchedulePage);
            _logger.Log($"FavScheduleButton clicked");
        }

        private Filters GenerateDefaultFilters()
        {
            return new Filters
            {
                GroupBy = GroupByEnum.Status,
                OrderBy = OrderByEnum.Name
            };
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
