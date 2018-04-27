using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using showTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.FavouritiesSchedulePage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavouritiesSchedulePage : ContentPage
	{
		public FavouritiesSchedulePage ()
		{
			InitializeComponent ();

		    MessagingCenter.Subscribe<FavouritiesScheduleViewModel>(this, Constants.PopupAlertKey,
		        model => DisplayAlert(model.PopupAlertTitle, model.PopupAlertMessage, Constants.OkButtonText));


            DateRangeControl.SetValuesToDefault();
		    DateRangeControl.MinimumHeightRequest = DateRangeControl.Height;

		    Task.Run(() =>
		    {
		        Task.Delay(10);
		        ((FavouritiesScheduleViewModel)BindingContext)?.OnGenerateRequested?.Execute(null);
            });		    
        }
	}
}