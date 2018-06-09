using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using showTracker.BusinessLayer.Extensions;
using showTracker.Model;
using showTracker.Model.Enum;
using showTracker.Model.Filters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterEditorControl : Grid
	{
        private static readonly Filters InitFilters = new Filters();
	    public static readonly BindableProperty FiltersProperty =
	        BindableProperty.Create(nameof(Filters), typeof(Filters), typeof(FilterEditorControl), InitFilters, BindingMode.TwoWay);

	    public Filters Filters
	    {
	        get => (Filters) GetValue(FiltersProperty);
	        set => SetValue(FiltersProperty, value);
	    }

	    public static readonly BindableProperty DefaultFiltersProperty =
	        BindableProperty.Create(nameof(DefaultFilters), typeof(Filters), typeof(FilterEditorControl));

	    public Filters DefaultFilters
	    {
	        get => (Filters) GetValue(DefaultFiltersProperty);
	        set => SetValue(DefaultFiltersProperty, value);
	    }

	    public static readonly BindableProperty FiltersAcceptedProperty =
	        BindableProperty.Create(nameof(FiltersAccepted), typeof(ICommand), typeof(FilterEditorControl));

	    public ICommand FiltersAccepted
	    {
	        get => (ICommand) GetValue(FiltersAcceptedProperty);
	        set => SetValue(FiltersAcceptedProperty, value);
	    }

	    public static readonly BindableProperty FiltersDiscardedProperty =
	        BindableProperty.Create(nameof(FiltersDiscarded), typeof(ICommand), typeof(FilterEditorControl));

	    public ICommand FiltersDiscarded
	    {
	        get => (ICommand) GetValue(FiltersDiscardedProperty);
	        set => SetValue(FiltersDiscardedProperty, value);
	    }

	    public static readonly BindableProperty IsShowProperty =
	        BindableProperty.Create(nameof(IsShow), typeof(bool), typeof(FilterEditorControl), false,
	            propertyChanged: (bindeable, value, newValue) =>
	            {
	                if ((bool) newValue)
	                {
	                    var groupByList = ((FilterEditorControl) bindeable).GroupByList;
	                    groupByList.Remove(Enum.GetName(typeof(GroupByEnum), GroupByEnum.AirDate).SplitCamelCase());
	                    groupByList.Remove(Enum.GetName(typeof(GroupByEnum), GroupByEnum.AirTime).SplitCamelCase());
	                }
	            });

	    public bool IsShow
	    {
	        get => (bool) GetValue(IsShowProperty);
	        set => SetValue(IsShowProperty, value);
	    }

	    public ObservableCollection<string> OrderByList { get; }
	    public ObservableCollection<string> GroupByList { get; }
	    public ObservableCollection<string> StatusList { get; }

	    public string OrderByLabel => Constants.OrderByLabel;
	    public string MinRatingLabel => Constants.MinRatingLabel;
	    public string MinRuntimeLabel => Constants.MinRuntimeLabel;
	    public string StatusLabel => Constants.StatusLabel;
	    public string GenreLabel => Constants.GenreLabel;
	    public string GroupByLabel => Constants.GroupByLabel;

	    public string ClearIcon => Constants.ClearIconResourceId;
	    public string OkIcon => Constants.OkIconResourceId;
	    public string AscendingIcon => Constants.AscendingIconResourceId;
	    public string DescendingIcon => Constants.DescendingIconResourceId;

        public FilterEditorControl ()
		{
			InitializeComponent ();

		    OrderByList = new ObservableCollection<string>(Enum.GetNames(typeof(OrderByEnum)).Select(x => x.SplitCamelCase()).ToList());
		    GroupByList = new ObservableCollection<string>(Enum.GetNames(typeof(GroupByEnum)).Select(x => x.SplitCamelCase()).ToList());
		    StatusList = new ObservableCollection<string>(Enum.GetNames(typeof(StatusEnum)).Select(x => x.SplitCamelCase()).ToList());            
        }

	    private void ClearRequested(object sender, EventArgs e)
	    {
	        Filters.Genre = DefaultFilters?.Genre ?? string.Empty;
	        Filters.GroupBy = DefaultFilters?.GroupBy ?? GroupByEnum.None;
	        Filters.MinRating = DefaultFilters?.MinRating ?? 0;
	        Filters.MinRuntime = DefaultFilters?.MinRuntime ?? 0;
	        Filters.OrderBy = DefaultFilters?.OrderBy ?? OrderByEnum.None;
	        Filters.Status = DefaultFilters?.Status ?? StatusEnum.None;
	        Filters.IsOrderByAscending = DefaultFilters?.IsOrderByAscending ?? true;
	    }

	    private void ChangeOrderDirection(object sender, EventArgs e)
	    {
	        Filters.IsOrderByAscending = !Filters.IsOrderByAscending;
	    }
	}
}