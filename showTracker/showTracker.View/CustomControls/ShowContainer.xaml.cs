using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Windows.Input;
using CommonServiceLocator;
using showTracker.BusinessLayer.Extensions;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Constants = showTracker.Model.Constants;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowContainer : Grid
	{
        #region BindeableProperties

	    public static readonly BindableProperty ShowsCollectionProperty =
	        BindableProperty.Create(nameof(ShowsCollection), typeof(IList<ShowDto>), typeof(ShowContainer),
	            propertyChanged: (bindable, value, newValue) =>
	            {
	                ((ShowContainer)bindable).GroupShowsByProperty();
	            });

	    public IList<ShowDto> ShowsCollection
	    {
	        get => (IList<ShowDto>)GetValue(ShowsCollectionProperty);
	        set
	        {
	            SetValue(ShowsCollectionProperty, value);
	            GroupShowsByProperty();
	        }
	    }

	    public static readonly BindableProperty SingleItemHeightProperty =
	        BindableProperty.Create(nameof(SingleItemHeight), typeof(double), typeof(ShowContainer), Constants.DefaultShowContainerItemHeight);

	    public double SingleItemHeight
	    {
	        get => (double)GetValue(SingleItemHeightProperty);
	        set => SetValue(SingleItemHeightProperty, value);
	    }

	    public static readonly BindableProperty GroupByProperty =
	        BindableProperty.Create(nameof(GroupBy), typeof(string), typeof(ShowContainer),
	            propertyChanged: (bindable, value, newValue) =>
	            {
	                ((ShowContainer)bindable).GroupShowsByProperty();
	            });

	    public string GroupBy
	    {
	        get => (string)GetValue(GroupByProperty);
	        set
	        {
	            SetValue(GroupByProperty, value);
	            GroupShowsByProperty();
	        }
	    }

	    public static readonly BindableProperty RefreshCommandProperty =
	        BindableProperty.Create(nameof(RefreshCommand), typeof(ICommand), typeof(ShowContainer), null,
	            propertyChanged: (bindable, value, newValue) =>
	            {
	                ((ShowContainer) bindable).ViewModel.RefreshAvaliable = newValue != null;
	            });

	    public ICommand RefreshCommand
	    {
	        get => (ICommand) GetValue(RefreshCommandProperty);
	        set => SetValue(RefreshCommandProperty, value);
	    }

        #endregion

	    public bool AnyShowsInCollection
	    {
	        get => false;
	        private set => ViewModel.AnyEntityInCollection = value;
	    }

        public string NoItemsString => Constants.NoItemsInCollection;
        public EntityContainerViewModel ViewModel { get; }

	    private readonly IJsonSerializeService _jsonSerializeService;
	    private readonly ISTLogger _logger;
		public ShowContainer ()
		{
		    _jsonSerializeService = ServiceLocator.Current.GetInstance<IJsonSerializeService>();
		    _logger = ServiceLocator.Current.GetInstance<ISTLogger>();

		    ViewModel = new EntityContainerViewModel(_jsonSerializeService, _logger)
		    {
		        RefreshCommandWrapper = new Command(Refresh)
		    };
		    InitializeComponent ();
		}

	    private void GroupShowsByProperty()
	    {
	        if (ShowsCollection.IsNullOrEmpty())
	        {
	            AnyShowsInCollection = false;
                ViewModel.GroupedResults = new List<ObservableCollection<ShowDto>>().AsQueryable();
                return;
	        }

	        AnyShowsInCollection = true;
	        if (string.IsNullOrEmpty(GroupBy))
	        {
	            GenerateGroupedResultWithOneMainGroup();
                _logger.Log($"GroupBy is empty");
	        }
	        else
	        {
	            try
	            {
	                GenerateGroupedResultUsingGroupProperty();
	            }
	            catch (Exception exception)
	            {
	                GenerateGroupedResultWithOneMainGroup();
                    _logger.Log($"Msg: {exception.Message}");
                    _logger.Log($"ST: {exception.StackTrace}");
                    _logger.Log($"Src: {exception.Source}");
	                _logger.Log($"HR: {exception.HResult}");
	                _logger.Log($"Exception: {exception}");
                }
            }
	    }

	    private void GenerateGroupedResultWithOneMainGroup()
	    {

	        ViewModel.GroupedResults = ShowsCollection.AsQueryable();

	        ViewModel.IsGroupNameVisible = false;
        }

        private void GenerateGroupedResultUsingGroupProperty()
	    {
	        ViewModel.GroupedResults = ShowsCollection
	            .AsQueryable()
	            .GroupBy(GroupBy, "it");

	        ViewModel.IsGroupNameVisible = true;
        }

	    private void Refresh()
	    {
	        ViewModel.IsRefreshing = true;

            RefreshCommand?.Execute(null);

	        ViewModel.IsRefreshing = false;
	    }
    }
}