using System;
using System.Windows.Input;
using showTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DateSearchControl : Grid
	{	    
	    public static readonly BindableProperty CurrentDateSelectedProperty =
	        BindableProperty.Create(nameof(CurrentDateSelected), typeof(DateTime), typeof(DateSearchControl), DateTime.Today, BindingMode.TwoWay);

	    public DateTime CurrentDateSelected
	    {
	        get => (DateTime) GetValue(CurrentDateSelectedProperty);
	        set => SetValue(CurrentDateSelectedProperty, value);
	    }

	    public static readonly BindableProperty SearchRequestedProperty =
	        BindableProperty.Create(nameof(SearchRequested), typeof(ICommand), typeof(SearchControl));

	    public ICommand SearchRequested
	    {
	        get => (ICommand)GetValue(SearchRequestedProperty);
	        set => SetValue(SearchRequestedProperty, value);
	    }

	    public virtual void OnSearchRequested(object sender, EventArgs args)
	    {
	        SearchRequested?.Execute(null);
	    }

	    public string SearchIcon => Constants.SearchIconResourceId;
	    public string DateFormat => "D";

	    public DateSearchControl()
	    {
	        InitializeComponent();
	    }

	    public void SetDateToToday()
	    {
	        CurrentDateSelected = DateTime.Today;
	    }
    }
}