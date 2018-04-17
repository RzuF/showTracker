using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        #endregion

        public bool AnyEpisodesInCollection
        {
            get => false;
            private set => ViewModel.AnyEntityInCollection = value;
        }

        public string NoItemsString => Constants.NoItemsInCollection;
        public EntityContainerViewModel ViewModel { get; }

        private readonly IJsonSerializeService _jsonSerializeService;
        private readonly ISTLogger _logger;
        public EpisodeContainer()
        {
            _jsonSerializeService = ServiceLocator.Current.GetInstance<IJsonSerializeService>();
            _logger = ServiceLocator.Current.GetInstance<ISTLogger>();
            ViewModel = new EntityContainerViewModel(_jsonSerializeService, _logger);
            InitializeComponent();
        }

        private void GroupEpisodesByProperty()
        {            
            if (EpisodeCollection.IsNullOrEmpty())
            {
                AnyEpisodesInCollection = false;
                ViewModel.GroupedResults = new List<ObservableCollection<EpisodeDto>>().AsQueryable();
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
        }

        private void GenerateGroupedResultWithOneMainGroup()
        {
            ViewModel.GroupedResults = EpisodeCollection.AsQueryable();

            ViewModel.IsGroupNameVisible = false;
        }

        private void GenerateGroupedResultUsingGroupProperty()
        {
            ViewModel.GroupedResults = EpisodeCollection
                .AsQueryable()
                .GroupBy(GroupBy, "it");

            ViewModel.IsGroupNameVisible = true;
        }
	}
}