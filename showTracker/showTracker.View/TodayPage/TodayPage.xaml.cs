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

            DateSearchControl.SetDateToToday();
	        DateSearchControl.MinimumHeightRequest = DateSearchControl.Height;
	    }
	}
}