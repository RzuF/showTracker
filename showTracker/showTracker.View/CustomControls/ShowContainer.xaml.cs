using System.Collections.Generic;
using showTracker.Model.API.Dto;
using showTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowContainer : Grid
	{

	    public static readonly BindableProperty ShowsCollectionProperty =
	        BindableProperty.Create(nameof(ShowsCollection), typeof(IEnumerable<ShowDto>), typeof(ShowContainer));

	    public IEnumerable<ShowDto> ShowsCollection
	    {
	        get => (IEnumerable<ShowDto>) GetValue(ShowsCollectionProperty);
	        set => SetValue(ShowsCollectionProperty, value);
	    }

	    public static readonly BindableProperty SingleItemHeightProperty =
	        BindableProperty.Create(nameof(SingleItemHeight), typeof(double), typeof(ShowContainer), Constants.DefaultShowContainerItemHeight);

	    public double SingleItemHeight
	    {
	        get => (double) GetValue(SingleItemHeightProperty);
	        set => SetValue(SingleItemHeightProperty, value);
	    }
		public ShowContainer ()
		{
			InitializeComponent ();
		}
	}
}