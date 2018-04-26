using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DateRangeControl : Grid
	{
	    public static readonly BindableProperty StartDateProperty =
	        BindableProperty.Create(nameof(StartDate), typeof(DateTime), typeof(DateRangeControl), DateTime.Today);

	    public DateTime StartDate
	    {
	        get => (DateTime) GetValue(StartDateProperty);
	        set => SetValue(StartDateProperty, value);
	    }

	    public static readonly BindableProperty EndDateProperty =
	        BindableProperty.Create(nameof(EndDate), typeof(DateTime), typeof(DateRangeControl), DateTime.Today.AddDays(7));

	    public DateTime EndDate
	    {
	        get => (DateTime) GetValue(EndDateProperty);
	        set => SetValue(EndDateProperty, value);
	    }

	    public static readonly BindableProperty GenerateRequestProperty =
	        BindableProperty.Create(nameof(GenerateRequest), typeof(ICommand), typeof(DateRangeControl));

	    public ICommand GenerateRequest
	    {
	        get => (ICommand) GetValue(GenerateRequestProperty);
	        set => SetValue(GenerateRequestProperty, value);
	    }

	    public string GenerateLabel => "Generate";
	    public string StartText => "Start:";
	    public string EndTest => "End:";
	    public string DateFormat => "D";
        public DateRangeControl ()
		{
			InitializeComponent ();
		}

	    public void SetValuesToDefault()
	    {
	        StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(7);
	    }
	}
}