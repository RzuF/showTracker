using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
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
	        BindableProperty.Create(nameof(ShowsCollection), typeof(IEnumerable<ShowDto>), typeof(ShowContainer),
	            propertyChanged: (bindable, value, newValue) =>
	            {
	                ((ShowContainer)bindable).GroupShowsByProperty();
	            });

	    public IEnumerable<ShowDto> ShowsCollection
	    {
	        get => (IEnumerable<ShowDto>)GetValue(ShowsCollectionProperty);
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

        #endregion

	    public bool AnyShowsInCollection { get; private set; } = true;

	    public string NoItemsString => Constants.NoItemsInCollection;
        public ShowConatinerViewModel ViewModel { get; }

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
	}
}