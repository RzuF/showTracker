using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Windows.Input;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using showTracker.Model.View;
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

	    public static readonly BindableProperty AllItemsLoadedProperty =
	        BindableProperty.Create(nameof(AllItemsLoaded), typeof(ICommand), typeof(ShowContainer));

	    public ICommand AllItemsLoaded
	    {
	        get => (ICommand) GetValue(AllItemsLoadedProperty);
	        set => SetValue(AllItemsLoadedProperty, value);
	    }

        #endregion

	    public bool AnyShowsInCollection
	    {
	        get => false;
	        private set => ViewModel.AnyShowsInCollection = value;
	    }

	    public string NoItemsString => Constants.NoItemsInCollection;
        public ShowConatinerViewModel ViewModel { get; }

	    private ShowDto _lastShow = null;

	    private readonly IJsonSerializeService _jsonSerializeService;
	    private readonly ISTLogger _logger;
		public ShowContainer ()
		{
		    _jsonSerializeService = ServiceLocator.Current.GetInstance<IJsonSerializeService>();
		    _logger = ServiceLocator.Current.GetInstance<ISTLogger>();
            ViewModel = new ShowConatinerViewModel(_jsonSerializeService, _logger);
		    InitializeComponent ();
		}

	    private void GroupShowsByProperty()
	    {
	        if (!ShowsCollection.Any())
	        {
	            AnyShowsInCollection = false;
                ViewModel.GroupedResults = new List<GroupedResult<ShowDto>>();
                AllItemsLoaded?.Execute(null);
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

	        _lastShow = ViewModel.GroupedResults.Last().Results.Last();
	    }

	    private void GenerateGroupedResultWithOneMainGroup()
	    {
            
	        ViewModel.GroupedResults = new List<GroupedResult<ShowDto>>
	        {
	            new GroupedResult<ShowDto>
                {
	                GroupName = "All",
	                Results = ShowsCollection
	            }
	        };
	        ViewModel.IsGroupNameVisible = false;
        }

        private void GenerateGroupedResultUsingGroupProperty()
	    {
	        var groupedShows = ShowsCollection
                .AsQueryable()
                .GroupBy(GroupBy, "it")
                .Select<GroupedResult<ShowDto>>($"new (it.Key.ToString() as {GroupedResult<ShowDto>.GroupNameString}, it as {GroupedResult<ShowDto>.ResultsString})");

	        var jsonSerializedGroupedShows = _jsonSerializeService.SerializeObject(groupedShows);
	        ViewModel.GroupedResults = _jsonSerializeService.DeserializeObject<IEnumerable<GroupedResult<ShowDto>>>(jsonSerializedGroupedShows).ToList();

	        ViewModel.IsGroupNameVisible = true;
	    }

	    private void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
	    {
	        var show = (ShowDto) e.Item;
            _logger.Log(show.Name);

	        if (show == _lastShow)
	        {
                AllItemsLoaded?.Execute(null);
	        }
	    }

	    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        if (e.Item == null)
	        {
	            return;
	        }
	        ((ListView)sender).SelectedItem = null;
        }
	}
}