using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
                Debug.WriteLine($"_groupedResults: {_jsonSerializeService.SerializeObject(_groupedResults)}");
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

        #endregion

        private readonly IJsonSerializeService _jsonSerializeService;

        public ShowConatinerViewModel(IJsonSerializeService jsonSerializeService)
        {
            _jsonSerializeService = jsonSerializeService;
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.WriteLine($"PropertyChanged: {propertyName}");
        }

        #endregion

        
    }
}
