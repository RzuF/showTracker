using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		    DateRangeControl.SetValuesToDefault();
		    DateRangeControl.MinimumHeightRequest = DateRangeControl.Height;
        }
	}
}