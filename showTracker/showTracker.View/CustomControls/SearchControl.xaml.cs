using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using showTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchControl : Grid
	{
	    private const string DefaultPlaceholder = "Type search phrase...";

	    public static readonly BindableProperty SearchPhraseProperty =
	        BindableProperty.Create(nameof(SearchPhrase), typeof(string), typeof(SearchControl), "", BindingMode.TwoWay);

	    public string SearchPhrase
	    {
	        get => (string) GetValue(SearchPhraseProperty);
	        set => SetValue(SearchPhraseProperty, value);
	    }

	    public static readonly BindableProperty PlaceholderProperty =
	        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(SearchControl), DefaultPlaceholder);

	    public string Placeholder
	    {
	        get => (string) GetValue(PlaceholderProperty);
	        set => SetValue(PlaceholderProperty, value);
	    }

	    public static readonly BindableProperty SearchRequestedProperty =
	        BindableProperty.Create(nameof(SearchRequested), typeof(ICommand), typeof(SearchControl));

	    public ICommand SearchRequested
	    {
	        get => (ICommand) GetValue(SearchRequestedProperty);
	        set => SetValue(SearchRequestedProperty, value);
	    }

	    public virtual void OnSearchRequested(object sender, EventArgs args)
	    {
	        SearchRequested?.Execute(null);
	    }

	    public string SearchIcon => Constants.SearchIconResourceId;

		public SearchControl ()
		{
			InitializeComponent ();
		}

	    public void FocusEntry()
	    {
            SearchEntry.Unfocus();
	        SearchEntry.Focus();
	    }
	}
}