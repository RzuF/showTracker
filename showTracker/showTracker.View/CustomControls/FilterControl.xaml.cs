using System.ComponentModel;
using System.Windows.Input;
using showTracker.Model;
using showTracker.Model.Enum;
using showTracker.Model.Filters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterControl : Grid
	{
	    public LayoutOptions FilterOption => LayoutOptions.FillAndExpand;

	    public static readonly BindableProperty FiltersProperty =
	        BindableProperty.Create(nameof(Filters), typeof(Filters), typeof(FilterControl), defaultBindingMode: BindingMode.TwoWay, 
	            propertyChanged: (bindable, value, newValue) =>
	            {
	                if (value != null)
	                {
	                    ((Filters)value).PropertyChanged -=
	                        ((FilterControl)bindable).FiltersOnPropertyChanged;
                    }

	                if (newValue != null)
	                {
	                    ((Filters)newValue).PropertyChanged +=
	                        ((FilterControl)bindable).FiltersOnPropertyChanged;
                    }
	            });

	    public Filters Filters
	    {
	        get => (Filters) GetValue(FiltersProperty);
	        set => SetValue(FiltersProperty, value);
	    }

	    public static readonly BindableProperty FiltersChangedProperty =
	        BindableProperty.Create(nameof(FiltersChanged), typeof(ICommand), typeof(FilterControl));

	    public ICommand FiltersChanged
	    {
	        get => (ICommand) GetValue(FiltersChangedProperty);
	        set => SetValue(FiltersChangedProperty, value);
	    }

	    public static readonly BindableProperty FiltersModifyRequestedProperty =
	        BindableProperty.Create(nameof(FiltersModifyRequested), typeof(ICommand), typeof(FilterControl));

	    public ICommand FiltersModifyRequested
	    {
	        get => (ICommand) GetValue(FiltersModifyRequestedProperty);
	        set => SetValue(FiltersModifyRequestedProperty, value);
	    }

        public FilterViewModel ViewModel { get; }

	    public string FilterIconResourceId => Constants.FilterIconResourceId;
	    public string FilterColoredIconResourceId => Constants.FilterColoredIconResourceId;
        public FilterControl ()
		{
			InitializeComponent ();

            ViewModel = new FilterViewModel();
		}

	    private void FiltersOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
	    {
            FiltersChanged?.Execute(null);
	        if (Filters.OrderBy == OrderByEnum.None && Filters.MinRating == 0 && Filters.Status == StatusEnum.None && Filters.MinRuntime == 0 && Filters.Genre == "" && Filters.GroupBy == GroupByEnum.None)
	        {
	            ViewModel.IsIconFilterColored = false;
	        }
	        else
	        {
                ViewModel.IsIconFilterColored = true;
	        }
	    }
	}
}