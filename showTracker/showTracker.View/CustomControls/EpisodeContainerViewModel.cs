using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.ViewModel.CustomControls
{
    public class EpisodeContainerViewModel : INotifyPropertyChanged
    {
        #region Properties

        private List<GroupedResult<EpisodeDto>> _groupedResults;
        public List<GroupedResult<EpisodeDto>> GroupedResults
        {
            get => _groupedResults;
            set
            {
                _groupedResults = value;
                _logger.Log($"_groupedResults: {_jsonSerializeService.SerializeObject(_groupedResults)}");
                OnPropertyChanged();

                LazyGroupedResults = new ObservableCollection<GroupedResult<EpisodeDto>>();
                CanLoadMore = value.Any();
                Task.Run(() => LoadNextBatch());
            }
        }

        private ObservableCollection<GroupedResult<EpisodeDto>> _lazyGroupedResults;
        public ObservableCollection<GroupedResult<EpisodeDto>> LazyGroupedResults
        {
            get => _lazyGroupedResults;
            set
            {
                _lazyGroupedResults = value;
                OnPropertyChanged();
            }
        }

        private bool _isGroupNameVisible;
        public bool IsGroupNameVisible
        {
            get => _isGroupNameVisible;
            set
            {
                _isGroupNameVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _anyEpisodesInCollection;
        public bool AnyEpisodesInCollection
        {
            get => _anyEpisodesInCollection;
            set
            {
                _anyEpisodesInCollection = value;
                OnPropertyChanged();
            }
        }

        private bool _canLoadMore;
        public bool CanLoadMore
        {
            get => _canLoadMore;
            set
            {
                _canLoadMore = value;
                OnPropertyChanged();
            }
        }

        private Color _evenColor;
        public Color EvenColor
        {
            get => _evenColor;
            set
            {
                _evenColor = value;
                OnPropertyChanged();
            }
        }

        private Color _oddColor;
        public Color OddColor
        {
            get => _oddColor;
            set
            {
                _oddColor = value;
                OnPropertyChanged();
            }
        }

        public EpisodeDto LastAddedItem;

        #endregion

        private readonly int _batchSize;

        private readonly IJsonSerializeService _jsonSerializeService;
        private readonly ISTLogger _logger;        

        public EpisodeContainerViewModel(IJsonSerializeService jsonSerializeService, ISTLogger logger)
        {
            _jsonSerializeService = jsonSerializeService;
            _logger = logger;

            OddColor = new Color(0, 0, 0, 0.05);
            EvenColor = Color.Transparent;

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    _batchSize = 1000;
                    break;
                default:
                    _batchSize = 10;
                    break;
            }
        }

        public void LoadNextBatch()
        {
            int batchSize = _batchSize;

            foreach (var groupedResult in GroupedResults)
            {
                var batch = groupedResult.Results.Take(batchSize).ToList();
                ObservableCollection<EpisodeDto> lazyGroupedCollection;
                if (LazyGroupedResults.Any(x => x.GroupName == groupedResult.GroupName))
                {
                    lazyGroupedCollection =
                        LazyGroupedResults.First(x => x.GroupName == groupedResult.GroupName).Results;
                }
                else
                {
                    lazyGroupedCollection = new ObservableCollection<EpisodeDto>();
                    LazyGroupedResults.Add(new GroupedResult<EpisodeDto>
                    {
                        GroupName = groupedResult.GroupName,
                        Results = lazyGroupedCollection
                    });
                }

                LastAddedItem = batch.Last();

                for (int i = 0; i < batch.Count; i++)
                {
                    groupedResult.Results.RemoveAt(i);
                    lazyGroupedCollection.Add(batch[i]);
                }

                batchSize -= batch.Count;                

                if (batchSize <= 0)
                {
                    break;
                }
            }

            var group = GroupedResults.FirstOrDefault();
            if (group != null && group.Results.Count == 0)
            {
                GroupedResults.Remove(group);
                if (GroupedResults.Count == 0)
                {
                    CanLoadMore = false;
                }
            }
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _logger.Log($"PropertyChanged: {propertyName}");
        }

        #endregion


    }
}
