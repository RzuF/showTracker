using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.SearchPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
		public SearchPage ()
		{
			InitializeComponent ();
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        SearchControl.FocusEntry();
	        SearchControl.MinimumHeightRequest = SearchControl.Height;
	    }
	}
}