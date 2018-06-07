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
        public string SummaryTabLabel => "Summary";
        public string CastTabLabel => "Cast";
        public string EpisodesTabLabel => "Episodes";
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

        private readonly ISTLogger _stLogger;
        private readonly IApiClientService _apiClientService;
        private readonly IFavouritiesService _favouritiesService;

        public ShowViewModel(ISTLogger stLogger, IApiClientService apiClientService, IFavouritiesService favouritiesService)
        {
            _stLogger = stLogger;
            _apiClientService = apiClientService;
            _favouritiesService = favouritiesService;

            OnFavouriteToggled = new Command(FavouriteToggled);

            PageTitle = "Show is loading";
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
                UriImageSource = new UriImageSource
                {
                    CachingEnabled = true,
                    Uri = new Uri(show.Image.MediumImgUrl)
                };
                IsFavourite = _favouritiesService.IsFavourite(show.Id);
                LoadFailed = false;
                Task.Factory.StartNew(() =>
                {
                    var seasons = show.Seasons.Select(season => new SeasonCarouselModel
                        {
                            CarouselPageTitle = $"Season {season.Number.GetValueOrDefault()}",
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
                _stLogger.Log($"Exception: {invalidShowException.Message}\n\nStackTrace: {invalidShowException.StackTrace}");

                PopupAlertTitle = Constants.WrongQuery;
                PopupAlertMessage = Constants.ErrorDuringFetchingShow;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
                LoadFailed = true;
            }
            catch (HttpRequestException httpRequestException)
            {
                _stLogger.Log($"Exception: {httpRequestException.Message}\n\nStackTrace: {httpRequestException.StackTrace}");

                PopupAlertTitle = Constants.NoInternetConnection;
                PopupAlertMessage = Constants.CheckYourInternetConnection;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
                LoadFailed = true;
            }

            catch (Exception exception)
            {
                _stLogger.Log($"Exception: {exception.Message}\n\nStackTrace: {exception.StackTrace}");

                PopupAlertTitle = Constants.UndefinedError;
                PopupAlertMessage = Constants.PleaseContactDeveloper;
                MessagingCenter.Send(this, Constants.PopupAlertKey);
                LoadFailed = true;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void FavouriteToggled()
        {
            var action = _favouritiesService.AddOrDelete(Show);
            _stLogger.Log($"{Show?.Name}: {action}");
            IsFavourite = action == FavouritiesAction.Add;
        }
    }
}
