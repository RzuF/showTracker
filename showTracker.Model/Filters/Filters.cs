using System.ComponentModel;
using System.Runtime.CompilerServices;
using showTracker.Model.Annotations;
using showTracker.Model.Enum;

namespace showTracker.Model.Filters
{
    public class Filters : INotifyPropertyChanged

    {
        private OrderByEnum _orderBy = OrderByEnum.None;
        private double _minRating;
        private StatusEnum _status = StatusEnum.None;
        private int _minRuntime;
        private string _genre = "";
        private GroupByEnum _groupBy = GroupByEnum.None;
        private bool _isOrderByAscending = true;

        public bool IsOrderByAscending
        {
            get => _isOrderByAscending;
            set
            {
                _isOrderByAscending = value;
                OnPropertyChanged();
            }
        }

        public OrderByEnum OrderBy
        {
            get => _orderBy;
            set
            {
                _orderBy = value;
                OnPropertyChanged();
            }
        }

        public double MinRating
        {
            get => _minRating;
            set
            {
                _minRating = value;
                OnPropertyChanged();
            }
        }

        public StatusEnum Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public int MinRuntime
        {
            get => _minRuntime;
            set
            {
                _minRuntime = value;
                OnPropertyChanged();
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged();
            }
        }

        public GroupByEnum GroupBy
        {
            get => _groupBy;
            set
            {
                _groupBy = value;
                OnPropertyChanged();
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
