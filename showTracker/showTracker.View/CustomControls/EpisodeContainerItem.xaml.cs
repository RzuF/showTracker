using System;
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

                    ((EpisodeContainerItem)bindable).IsFavourite = ((EpisodeContainerItem)bindable)._favouritiesService.IsFavourite(episode?.Show?.Id ?? -1);
                    logger.Log($"Show: {episode?.Show?.Name} ({episode?.Show?.Id}) - {((EpisodeContainerItem)bindable).IsFavourite}");
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

        public string FavouriteIcon => Constants.FavouriteIconResourceId;
        public string OkIcon => Constants.OkIconResourceId;
        public string EpisodeName => $"Title: {Episode?.Name ?? "Unknown"}";
        public string ShowName => $"Show: {Episode?.Show?.Name ?? "Unknown"}";
        public string NumberOfEpisode => $"S{(Episode?.Season ?? 0):D2}E{(Episode?.Number ?? 0):D2}";

        private readonly IFavouritiesService _favouritiesService;
        private readonly ISTLogger _logger;

        public string Runtime => $"{Episode?.Runtime?.ToString() ?? "??"} min";

        public EpisodeContainerItem()
        {
            _favouritiesService = ServiceLocator.Current.GetInstance<IFavouritiesService>();
            _logger = ServiceLocator.Current.GetInstance<ISTLogger>();
            InitializeComponent();
        }

        private void Favourite_Clicked(object sender, EventArgs e)
        {
            if (Episode != null)
            {
                var action = _favouritiesService.AddOrDelete(Episode.Show);
                _logger.Log($"{Episode?.Show?.Name}: {action}");
                IsFavourite = action == FavouritiesAction.Add;
            }            
        }
    }
}