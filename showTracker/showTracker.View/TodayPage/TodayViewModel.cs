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

namespace showTracker.ViewModel.TodayPage
{
    public class TodayViewModel : BaseViewModel
    {
        public ICommand OnSearchRequested { get; }
        public ICommand OnFiltersChanged { get; }
        public ICommand OnFiltersToggle { get; }

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
                ApplyFilters();
                OnPropertyChanged();
            }
        }

        private List<EpisodeDto> _filteredEpisodes;
        public List<EpisodeDto> FilteredEpisodes
        {
            get => _filteredEpisodes;
            set
            {
                _filteredEpisodes = value;
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

        public TodayViewModel(IApiClientService apiClientService, ISTLogger stLogger)
        {
            _apiClientService = apiClientService;
            _stLogger = stLogger;

            OnSearchRequested = new Command(SearchRequested);
            OnFiltersChanged = new Command(ApplyFilters);
            OnFiltersToggle = new Command(FiltersToggle);

            Filters = GenerateDefaultFilters();
            DefaultFilters = GenerateDefaultFilters();

            PageTitle = Constants.TodayPageTitle;
            Episodes = new List<EpisodeDto>();
        }

        private void ApplyFilters()
        {
            IsFilterEditorVisible = false;

            FilteredEpisodes = Episodes.ToList();

            FilteredEpisodes = FilteredEpisodes.Where(x =>
                    (!x.Show.Rating.HasValue || x.Show.Rating.Value >= Filters.MinRating) &&
                    (!x.Runtime.HasValue || x.Runtime.Value >= Filters.MinRuntime))
                .ToList();

            if (Filters.Genre != "")
            {
                FilteredEpisodes = FilteredEpisodes.Where(x => x.Show.Genres.Select(y => y.ToLower()).Contains(Filters.Genre.Trim().ToLower())).ToList();
            }

            if (Filters.Status != StatusEnum.None)
            {
                FilteredEpisodes = FilteredEpisodes.Where(x => x.Show.Status == Enum.GetName(typeof(StatusEnum),Filters.Status)).ToList();
            }

            switch (Filters.GroupBy)
            {
                case GroupByEnum.None:
                    GroupBy = null;
                    break;
                case GroupByEnum.Type:
                    GroupBy = "Show.Type";
                    break;
                case GroupByEnum.Status:
                    GroupBy = "Show.Status";
                    break;
                case GroupByEnum.PremieredYear:
                    GroupBy = "Show.PremieredNotNull.Year";
                    break;
                case GroupByEnum.Runtime:
                    GroupBy = "Runtime";
                    break;
                case GroupByEnum.AirDate:
                    GroupBy = "AirDate";
                    break;
                case GroupByEnum.AirTime:
                    GroupBy = "AirTime";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Filters.OrderBy != OrderByEnum.None)
            {
                var orderByString =
                    $"{(GroupBy == null ? "" : GroupBy + ",")} {Enum.GetName(typeof(OrderByEnum), Filters.OrderBy)} {(Filters.IsOrderByAscending ? "asc" : "desc")}"
                        .Replace(" Year", " Show.PremieredNotNull.Year")
                        .Replace(" Rating", "Show.Rating");
                FilteredEpisodes = FilteredEpisodes.AsQueryable()
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
                var episodes = await _apiClientService.GetEpisodes(SearchDate);
                _stLogger.LogWithSerialization(episodes);

                Episodes = episodes.ToList();
            }
            catch (InvalidEpisodeException invalidEpisodeException)
            {
                NotifyAboutException(Constants.WrongQuery, Constants.ErrorDuringFetchingShow, invalidEpisodeException);
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

        private Filters GenerateDefaultFilters()
        {
            return new Filters
            {
                GroupBy = GroupByEnum.AirTime
            };
        }

        private void NotifyAboutException(string title, string message, Exception exception)
        {
            _stLogger.Log($"Exception: {exception.Message}\n\nStackTrace: {exception.StackTrace}");

            PopupAlertTitle = title;
            PopupAlertMessage = message;
            MessagingCenter.Send(this, Constants.PopupAlertKey);
        }
    }
}
