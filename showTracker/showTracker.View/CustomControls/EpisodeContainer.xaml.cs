using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using showTracker.Model.View;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Constants = showTracker.Model.Constants;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EpisodeContainer : Grid
	{
        #region BindeableProperties

        public static readonly BindableProperty EpisodeCollectionProperty =
            BindableProperty.Create(nameof(EpisodeCollection), typeof(IList<EpisodeDto>), typeof(EpisodeContainer),
                propertyChanged: (bindable, value, newValue) =>
                {
                    ((EpisodeContainer)bindable).GroupEpisodesByProperty();
                });

        public IList<EpisodeDto> EpisodeCollection
        {
            get => (IList<EpisodeDto>)GetValue(EpisodeCollectionProperty);
            set
            {
                SetValue(EpisodeCollectionProperty, value);
                GroupEpisodesByProperty();
            }
        }

        public static readonly BindableProperty SingleItemHeightProperty =
            BindableProperty.Create(nameof(SingleItemHeight), typeof(double), typeof(EpisodeContainer), Constants.DefaultEpisodeContainerItemHeight);

        public double SingleItemHeight
        {
            get => (double)GetValue(SingleItemHeightProperty);
            set => SetValue(SingleItemHeightProperty, value);
        }

        public static readonly BindableProperty GroupByProperty =
            BindableProperty.Create(nameof(GroupBy), typeof(string), typeof(EpisodeContainer),
                propertyChanged: (bindable, value, newValue) =>
                {
                    ((EpisodeContainer)bindable).GroupEpisodesByProperty();
                });

        public string GroupBy
        {
            get => (string)GetValue(GroupByProperty);
            set
            {
                SetValue(GroupByProperty, value);
                GroupEpisodesByProperty();
            }
        }

        public static readonly BindableProperty AllItemsLoadedProperty =
            BindableProperty.Create(nameof(AllItemsLoaded), typeof(ICommand), typeof(EpisodeContainer));

        public ICommand AllItemsLoaded
        {
            get => (ICommand)GetValue(AllItemsLoadedProperty);
            set => SetValue(AllItemsLoadedProperty, value);
        }

	    public static readonly BindableProperty ItemsLoadingStartedProperty =
	        BindableProperty.Create(nameof(ItemsLoadingStarted), typeof(ICommand), typeof(EpisodeContainer));

	    public ICommand ItemsLoadingStarted
	    {
	        get => (ICommand) GetValue(ItemsLoadingStartedProperty);
	        set => SetValue(ItemsLoadingStartedProperty, value);
	    }

        #endregion

        public bool AnyEpisodesInCollection
        {
            get => false;
            private set => ViewModel.AnyEpisodesInCollection = value;
        }

        public string NoItemsString => Constants.NoItemsInCollection;
	    public string LoadMoreItems => Constants.LoadMoreItems;
        public EpisodeContainerViewModel ViewModel { get; }

        private EpisodeDto _firstEpisode = null;
        private EpisodeDto _lastEpisode = null;

        private readonly IJsonSerializeService _jsonSerializeService;
        private readonly ISTLogger _logger;
        public EpisodeContainer()
        {
            _jsonSerializeService = ServiceLocator.Current.GetInstance<IJsonSerializeService>();
            _logger = ServiceLocator.Current.GetInstance<ISTLogger>();
            ViewModel = new EpisodeContainerViewModel(_jsonSerializeService, _logger);
            InitializeComponent();
        }

        private void GroupEpisodesByProperty()
        {
            if (!EpisodeCollection.Any())
            {
                AnyEpisodesInCollection = false;
                ViewModel.GroupedResults = new List<GroupedResult<EpisodeDto>>();
                AllItemsLoaded?.Execute(null);
                return;
            }

            AnyEpisodesInCollection = true;
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

            _firstEpisode = ViewModel.GroupedResults.FirstOrDefault()?.Results.FirstOrDefault();
            _lastEpisode = ViewModel.GroupedResults.Last().Results.Last();
            _logger.Log("LastEpisode: ", true);
            _logger.LogWithSerialization(_lastEpisode);
        }

        private void GenerateGroupedResultWithOneMainGroup()
        {

            ViewModel.GroupedResults = new List<GroupedResult<EpisodeDto>>
            {
                new GroupedResult<EpisodeDto>
                {
                    GroupName = "All",
                    Results = new ObservableCollection<EpisodeDto>(EpisodeCollection)
                }
            };
            ViewModel.IsGroupNameVisible = false;
        }

        private void GenerateGroupedResultUsingGroupProperty()
        {
            var groupedEpisodes = EpisodeCollection
                .AsQueryable()
                .GroupBy(GroupBy, "it")
                .Select<GroupedResult<EpisodeDto>>($"new (it.Key.ToString() as {GroupedResult<EpisodeDto>.GroupNameString}, it as {GroupedResult<EpisodeDto>.ResultsString})");

            var jsonSerializedGroupedEpisodes = _jsonSerializeService.SerializeObject(groupedEpisodes);
            ViewModel.GroupedResults = _jsonSerializeService.DeserializeObject<IEnumerable<GroupedResult<EpisodeDto>>>(jsonSerializedGroupedEpisodes).ToList();

            ViewModel.IsGroupNameVisible = true;
        }

        private void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var episode = (EpisodeDto)e.Item;
            _logger.Log(episode.Name);

            if (episode == _firstEpisode)
            {
                ItemsLoadingStarted?.Execute(null);
                ((ListView)sender).InvalidateMeasureNonVirtual(InvalidationTrigger.MeasureChanged);
                _logger.Log($"Last Ep: {ViewModel.LastAddedItem.Name}");

                if (Device.RuntimePlatform == Device.UWP)
                {
                    AllItemsLoaded?.Execute(null);
                }
            }

            if (episode == ViewModel.LastAddedItem)
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

	    private void Button_OnClicked(object sender, EventArgs e)
	    {	        
	        Task.Run(() =>
	        {
	            ViewModel.LoadNextBatch();
	        });
	    }
	}
}