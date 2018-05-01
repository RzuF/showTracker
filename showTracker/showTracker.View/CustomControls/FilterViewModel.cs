using System.ComponentModel;
using System.Runtime.CompilerServices;
using showTracker.Model.Annotations;

namespace showTracker.ViewModel.CustomControls
{
    public class FilterViewModel: INotifyPropertyChanged
    {
        private bool _isIconFilterColored;

        public bool IsIconFilterColored
        {
            get => _isIconFilterColored;
            set
            {
                _isIconFilterColored = value;
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
