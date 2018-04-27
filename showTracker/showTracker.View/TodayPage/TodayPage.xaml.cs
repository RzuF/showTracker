using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.TodayPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodayPage : ContentPage
	{
		public TodayPage ()
		{
			InitializeComponent ();
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        MessagingCenter.Subscribe<TodayViewModel>(this, "PopupAlert",
	            model => DisplayAlert(model.PopupAlertTitle, model.PopupAlertMessage, "Ok"));

            DateSearchControl.SetDateToToday();
	        DateSearchControl.MinimumHeightRequest = DateSearchControl.Height;
	    }
	}
}