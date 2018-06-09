using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using showTracker.BusinessLayer.Interfaces;
using Xamarin.Forms;

namespace showTracker.ViewModel.CustomControls
{
    public class EntityContainerViewModel : INotifyPropertyChanged
    {
        #region Properties

        private IQueryable _groupedResults;
        public IQueryable GroupedResults
        {
            get => _groupedResults;
            set
            {
                _groupedResults = value;
                _logger.Log($"_groupedResults: ", true);
                //_logger.LogWithSerialization(_groupedResults);
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

        private bool _anyEntityInCollection;
        public bool AnyEntityInCollection
        {
            get => _anyEntityInCollection;
            set
            {
                _anyEntityInCollection = value;
                OnPropertyChanged();
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
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

        private Color _groupColor;
        public Color GroupColor
        {
            get => _groupColor;
            set
            {
                _groupColor = value;
                OnPropertyChanged();
            }
        }

        private ICommand _refreshCommandWrapper;
        public ICommand RefreshCommandWrapper
        {
            get => _refreshCommandWrapper;
            set
            {
                _refreshCommandWrapper = value;
                OnPropertyChanged();
            }
        }

        private bool _refreshAvaliable;
        public bool RefreshAvaliable
        {
            get => _refreshAvaliable;
            set
            {
                _refreshAvaliable = value;
                OnPropertyChanged();
            }
        }

        #endregion        

        private readonly IJsonSerializeService _jsonSerializeService;
        private readonly ISTLogger _logger;        

        public EntityContainerViewModel(IJsonSerializeService jsonSerializeService, ISTLogger logger)
        {
            _jsonSerializeService = jsonSerializeService;
            _logger = logger;

            OddColor = new Color(0, 0, 0, 0.05);
            EvenColor = Color.Transparent;
            GroupColor = Color.Accent.MultiplyAlpha(0.2);
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
