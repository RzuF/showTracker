using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using showTracker.BusinessLayer.Exceptions;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model;
using showTracker.Model.API.Dto;
using showTracker.Model.Enum;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.ShowPage
{
    public class ShowViewModel : BaseViewModel
    {
        public string SummaryTabLabel => Constants.SummaryTabLabel;
        public string CastTabLabel => Constants.CastTabLabel;
        public string EpisodesTabLabel => Constants.EpisodesTabLabel;
        public string FavouriteIcon => Constants.FavouriteIconResourceId;
        public string OkIcon => Constants.OkIconResourceId;

        public ICommand OnFavouriteToggled { get; }

        private FullShowDto _show;        
        public FullShowDto Show
        {
            get => _show;
            set
            {
                _show = value;
                OnPropertyChanged();
            }
        }

        private object _navigationMessage;        
        public override object NavigationMessage
        {
            get => _navigationMessage;
            set
            {
                _navigationMessage = value;
                if (value is int showId)
                {
                    GetShow(showId);
                }
            }
        }

        private bool _loadFailed;
        public bool LoadFailed
        {
            get => _loadFailed;
            set
            {
                _loadFailed = value;
                OnPropertyChanged();
            }
        }

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

        private UriImageSource _uriImageSource;
        public UriImageSource UriImageSource
        {
            get => _uriImageSource;
            set
            {
                _uriImageSource = value;
                OnPropertyChanged();
            }
        }

        private List<SeasonCarouselModel> _seasons;
        public List<SeasonCarouselModel> Seasons
        {
            get => _seasons;
            set
            {
                _seasons = value;
                OnPropertyChanged();
            }
        }

        private bool _isFavourite;
        public bool IsFavourite
        {
            get => _isFavourite;
            set
            {
                _isFavourite = value;
                OnPropertyChanged();
            }
        }

        public string TypeLabel => $"{Constants.Type}:";
        public string LanguageLabel => $"{Constants.Language}:";
        public string GenresLabel => $"{Constants.Genres}:";
        public string SeasonsLabel => $"{Constants.Seasons}:";
        public string NumberOfEpisodesLabel => $"{Constants.NumberOfEpisodes}:";
        public string PremieredLabel => $"{Constants.Premiered}:";
        public string EndedLabel => $"{Constants.Ended}:";
        public string NetworkLabel => $"{Constants.Network}:";

        private readonly ISTLogger _stLogger;
        private readonly IApiClientService _apiClientService;
        private readonly IFavouritesService _favouritesService;
        private readonly INavigationService _navigationService;

        public ShowViewModel(ISTLogger stLogger, IApiClientService apiClientService, IFavouritesService favouritesService, INavigationService navigationService)
        {
            _stLogger = stLogger;
            _apiClientService = apiClientService;
            _favouritesService = favouritesService;
            _navigationService = navigationService;

            OnFavouriteToggled = new Command(FavouriteToggled);

            PageTitle = Constants.ShowPageTitle;
        }

        private async void GetShow(int id)
        {
            IsLoading = true;
            try
            {
                var show = await _apiClientService.GetFullShow(id);
                _stLogger.LogWithSerialization(show);

                Show = show;
                PageTitle = show.Name;
                UriImageSource = show.Image?.MediumImgUrl == null ? null : new UriImageSource
                {
                    CachingEnabled = true,
                    Uri = new Uri(show.Image.MediumImgUrl)
                };
                IsFavourite = _favouritesService.IsFavourite(show.Id);
                LoadFailed = false;
                Task.Factory.StartNew(() =>
                {
                    var seasons = show.Seasons.Select(season => new SeasonCarouselModel
                        {
                            CarouselPageTitle = $"{Constants.Season} {season.Number.GetValueOrDefault()}",
                            Season = season,
                            Episodes = show.Episodes.ToList()
                                .Where(x => x.Season.GetValueOrDefault() == season.Number)
                                .Select(x =>
                                {
                                    var episode = x.Clone();
                                    episode.Show = show;
                                    return episode;
                                })
                                .ToList()
                        })
                        .ToList();

                    Seasons = seasons;
                });
            }
            catch (InvalidShowException invalidShowException)
            {
                NotifyAboutException(Constants.WrongQuery, Constants.ErrorDuringFetchingShow, invalidShowException);
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

        private void FavouriteToggled()
        {
            var action = _favouritesService.AddOrDelete(Show);
            _stLogger.Log($"{Show?.Name}: {action}");
            IsFavourite = action == FavouritiesAction.Add;
        }

        private void NotifyAboutException(string title, string message, Exception exception)
        {
            _stLogger.Log($"Exception: {exception.Message}\n\nStackTrace: {exception.StackTrace}");

            PopupAlertTitle = title;
            PopupAlertMessage = message;
            MessagingCenter.Send(this, Constants.PopupAlertKey);
            LoadFailed = true;

            _navigationService.NavigateBack();
        }
    }
}
