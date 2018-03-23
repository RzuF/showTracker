using System.Collections;
using CommonServiceLocator;

namespace showTracker.ViewModel.MainPage
{
    public class MainViewModel
    {
        public IEnumerable Shows { get; set; }
        public MainViewModel()
        {
        }
    }
}
