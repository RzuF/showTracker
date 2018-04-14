using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using showTracker.Model.View;

namespace showTracker.ViewModel.CustomControls
{
    public class ShowConatinerViewModel : INotifyPropertyChanged
    {
        #region Properties

        private List<GroupedResult<ShowDto>> _groupedResults;
        public List<GroupedResult<ShowDto>> GroupedResults
        {
            get => _groupedResults;
            set
            {
                _groupedResults = value;
                _logger.Log($"_groupedResults: {_jsonSerializeService.SerializeObject(_groupedResults)}");
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

        private bool _anyShowsInCollection;

        public bool AnyShowsInCollection
        {
            get => _anyShowsInCollection;
            set
            {
                _anyShowsInCollection = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private readonly IJsonSerializeService _jsonSerializeService;
        private readonly ISTLogger _logger;

        public ShowConatinerViewModel(IJsonSerializeService jsonSerializeService, ISTLogger logger)
        {
            _jsonSerializeService = jsonSerializeService;
            _logger = logger;
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
