using System;
using System.Windows.Input;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using showTracker.Model.Enum;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Constants = showTracker.Model.Constants;

namespace showTracker.ViewModel.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EpisodeContainerItem : ViewCell
    {
        public static readonly BindableProperty EpisodeProperty =
            BindableProperty.Create(nameof(Episode), typeof(EpisodeDto), typeof(EpisodeContainerItem),
                propertyChanged: (bindable, value, newValue) =>
                {
                    var logger = ((EpisodeContainerItem)bindable)._logger;
                    var episode = (EpisodeDto)newValue;

                    ((EpisodeContainerItem)bindable).IsFavourite = ((EpisodeContainerItem)bindable)._favouritesService.IsFavourite(episode?.Show?.Id ?? -1);
                });

        public EpisodeDto Episode
        {
            get => (EpisodeDto) GetValue(EpisodeProperty);
            set => SetValue(EpisodeProperty, value);
        }

        public static readonly BindableProperty ItemHeightProperty =
            BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(EpisodeContainerItem), Constants.DefaultShowContainerItemHeight);

        public double ItemHeight
        {
            get => (double) GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        private bool _isFavourite;
        public bool IsFavourite
        {
            get => _isFavourite;
            set
            {
                _isFavourite = value;
                OnPropertyChanged(nameof(IsFavourite));
            }
        }

        public static readonly BindableProperty CanNavigateToShowProperty =
            BindableProperty.Create(nameof(CanNavigateToShow), typeof(bool), typeof(EpisodeContainerItem), false,
                propertyChanged: (bindable, value, newValue) =>
                {
                    if ((bool)newValue)
                    {
                        ((EpisodeContainerItem)bindable).OnNavigateRequested = new Command(((EpisodeContainerItem)bindable).NavigateToShow);
                    }
                    else
                    {
                        ((EpisodeContainerItem)bindable).OnNavigateRequested = null;
                    }
                });

        public bool CanNavigateToShow
        {
            get => (bool) GetValue(CanNavigateToShowProperty);
            set => SetValue(CanNavigateToShowProperty, value);
        }

        public ICommand OnNavigateRequested
        {
            get => _onNavigateRequested;
            set
            {
                _onNavigateRequested = value;
                OnPropertyChanged(nameof(OnNavigateRequested));
            }
        }

        public string FavouriteIcon => Constants.FavouriteIconResourceId;
        public string OkIcon => Constants.OkIconResourceId;
        public string EpisodeName => $"{Constants.EpisodeTitle}: {Episode?.Name ?? Constants.Unknown}";
        public string ShowName => $"{Constants.ShowName}: {Episode?.Show?.Name ?? Constants.Unknown}";
        public string NumberOfEpisode => $"S{(Episode?.Season ?? 0):D2}E{(Episode?.Number ?? 0):D2}";

        private readonly IFavouritesService _favouritesService;
        private readonly ISTLogger _logger;
        private ICommand _onNavigateRequested;

        public string Runtime => $"{Episode?.Runtime?.ToString() ?? "??"} {Constants.MinutesShort}";

        public EpisodeContainerItem()
        {
            _favouritesService = ServiceLocator.Current.GetInstance<IFavouritesService>();
            _logger = ServiceLocator.Current.GetInstance<ISTLogger>();
            InitializeComponent();
        }

        private void NavigateToShow()
        {
            ServiceLocator.Current.GetInstance<INavigationService>().Navigate(ApplicationPageEnum.ShowPage, Episode.Show.Id);
        }

        private void Favourite_Clicked(object sender, EventArgs e)
        {
            if (Episode != null)
            {
                var action = _favouritesService.AddOrDelete(Episode.Show);
                _logger.Log($"{Episode?.Show?.Name}: {action}");
                IsFavourite = action == FavouritiesAction.Add;
            }            
        }
    }
}