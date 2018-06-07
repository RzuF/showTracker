using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Windows.Input;
using showTracker.BusinessLayer.Exceptions;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
using showTracker.Model.API.Dto;
using showTracker.Model.Enum;
using showTracker.Model.Filters;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.SearchPage
{
    public class SearchViewModel : BaseViewModel
    {
        public ICommand OnSearchRequested { get; }
        public ICommand OnFiltersChanged { get; }
        public ICommand OnFiltersToggle { get; }

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

        private readonly ISTLogger _stLogger;
        private readonly IApiClientService _apiClientService;

        public SearchViewModel(IApiClientService apiClientService, ISTLogger stLogger)
        {
            _apiClientService = apiClientService;
            _stLogger = stLogger;

            OnSearchRequested = new Command(SearchRequested);
            OnFiltersChanged = new Command(ApplyFilters);
            OnFiltersToggle = new Command(FiltersToggle);

            Filters = GenerateDefaultFilters();
            DefaultFilters = GenerateDefaultFilters();

            PageTitle = "Search for shows";
            Shows = new List<ShowDto>();
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
                FilteredShows = FilteredShows.Where(x => x.Genres.Select(y => y.ToLower()).Contains(Filters.Genre.ToLower())).ToList();
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

        private async void SearchRequested()
        {
            IsLoading = true;
            try
            {
                var shows = await _apiClientService.SearchShows(SearchPhrase);
                _stLogger.LogWithSerialization(shows);

                Shows = shows.ToList();
            }
            catch (InvalidShowException invalidShowException)
            {
                _stLogger.Log($"Exception: {invalidShowException.Message}\n\nStackTrace: {invalidShowException.StackTrace}");

                PopupAlertTitle = Constants.WrongQuery;
                PopupAlertMessage = Constants.ErrorDuringFetchingShow;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }
            catch (HttpRequestException httpRequestException)
            {
                _stLogger.Log($"Exception: {httpRequestException.Message}\n\nStackTrace: {httpRequestException.StackTrace}");

                PopupAlertTitle = Constants.NoInternetConnection;
                PopupAlertMessage = Constants.CheckYourInternetConnection;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }

            catch (Exception exception)
            {
                _stLogger.Log($"Exception: {exception.Message}\n\nStackTrace: {exception.StackTrace}");

                PopupAlertTitle = Constants.UndefinedError;
                PopupAlertMessage = Constants.PleaseContactDeveloper;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private Filters GenerateDefaultFilters()
        {
            return new Filters
            {
                
            };
        }
    }
}
