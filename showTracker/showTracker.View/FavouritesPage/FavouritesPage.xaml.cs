using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using showTracker.Model;
using showTracker.ViewModel.FavouritesPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.FavouritiesPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavouritiesPage : ContentPage
	{
		public FavouritiesPage ()
		{
			InitializeComponent ();
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        MessagingCenter.Subscribe<FavouritesViewModel>(this, Constants.PopupAlertKey,
	            model => DisplayAlert(model.PopupAlertTitle, model.PopupAlertMessage, Constants.OkButtonText));
        }
	}
}