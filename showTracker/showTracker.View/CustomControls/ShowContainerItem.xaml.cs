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
    public partial class ShowContainerItem : ViewCell
    {
        public static readonly BindableProperty ShowProperty =
            BindableProperty.Create(nameof(Show), typeof(ShowDto), typeof(ShowContainerItem),
                propertyChanged: (bindable, value, newValue) =>
                {
                    var show = (ShowDto) newValue;

                    ((ShowContainerItem)bindable).IsFavourite = ((ShowContainerItem)bindable)._favouritesService.IsFavourite(show?.Id ?? -1);
                });

        public ShowDto Show
        {
            get => (ShowDto) GetValue(ShowProperty);
            set => SetValue(ShowProperty, value);
        }

        public static readonly BindableProperty ItemHeightProperty = 
            BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(ShowContainerItem), Constants.DefaultShowContainerItemHeight);        

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
        public string Rating => $"{Show?.Rating?.ToString() ?? "??"}/{Constants.MaxRating}";
        public string Premiered => Show?.Premiered?.Year.ToString() ?? Constants.Unknown;

        private readonly IFavouritesService _favouritesService;
        private readonly ISTLogger _logger;
        private readonly INavigationService _navigationService;

        public ShowContainerItem ()
		{
		    _favouritesService = ServiceLocator.Current.GetInstance<IFavouritesService>();
            _logger = ServiceLocator.Current.GetInstance<ISTLogger>();
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            InitializeComponent ();
		}

        private void Favourite_Clicked(object sender, EventArgs e)
        {
            var action = _favouritesService.AddOrDelete(Show);
            _logger.Log($"{Show?.Name}: {action}");
            IsFavourite = action == FavouritiesAction.Add;
        }

        private void ShowClicked(object sender, EventArgs e)
        {
            _logger.Log($"{Show.Name} clicked, requested: ID -> {Show.Id}");
            _navigationService.Navigate(ApplicationPageEnum.ShowPage, Show?.Id);
        }
    }
}