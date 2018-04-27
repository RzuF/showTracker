using showTracker.Model;
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

	        MessagingCenter.Subscribe<SearchViewModel>(this, Constants.PopupAlertKey,
	            model => DisplayAlert(model.PopupAlertTitle, model.PopupAlertMessage, Constants.OkButtonText));

            SearchControl.FocusEntry();
	        SearchControl.MinimumHeightRequest = SearchControl.Height;
	    }
	}
}