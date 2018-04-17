using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Constants = showTracker.Model.Constants;

namespace showTracker.ViewModel.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EpisodeContainerItem : ViewCell
    {
        public static readonly BindableProperty EpisodeProperty =
            BindableProperty.Create(nameof(Episode), typeof(EpisodeDto), typeof(EpisodeContainerItem));

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

        public string FavouriteIcon => Constants.FavouriteIconResourceId;
        public string EpisodeName => $"Title: {Episode?.Name ?? "Unknown"}";
        public string ShowName => $"Show: {Episode?.Show?.Name ?? "Unknown"}";
        public string NumberOfEpisode => $"S{(Episode?.Season ?? 0):D2}E{(Episode?.Number ?? 0):D2}";

        public string Runtime => $"{Episode?.Runtime?.ToString() ?? "??"} min";

        public EpisodeContainerItem()
        {
            InitializeComponent();
        }
    }
}