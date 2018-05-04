using System.ComponentModel;
using System.Runtime.CompilerServices;
using showTracker.Model.Annotations;

namespace showTracker.Model.View
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _pageTitle;
        public virtual object NavigationMessage { get; set; }

        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }

        public string PopupAlertTitle { get; set; }
        public string PopupAlertMessage { get; set; }

        #region Notify Property Changed Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        
    }
}
