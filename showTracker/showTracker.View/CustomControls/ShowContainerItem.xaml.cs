using showTracker.Model;
using showTracker.Model.API.Dto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowContainerItem : Grid
    {
        public static readonly BindableProperty ShowProperty =
            BindableProperty.Create(nameof(Show), typeof(ShowDto), typeof(ShowContainerItem));

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

        public string FavouriteIcon => Constants.FavouriteIconResourceId;

        public ShowContainerItem ()
		{
			InitializeComponent ();
		}
	}
}