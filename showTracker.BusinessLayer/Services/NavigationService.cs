using System.Collections.Generic;
using System.Threading.Tasks;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.Enum;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.BusinessLayer.Services
{
    public class NavigationService : INavigationService
    {
        private Dictionary<ApplicationPageEnum, Page> _pageDictionary;

        public Dictionary<ApplicationPageEnum, Page> PageDictionary
        {
            get => _pageDictionary;
            set
            {
                if (_pageDictionary == null)
                {
                    _pageDictionary = value;
                }                
            }
        }

        public async Task Navigate(ApplicationPageEnum pageType, object message = null)
        {
            var page = PageDictionary[pageType];
            if (page.BindingContext is BaseViewModel viewModel)
            {
                viewModel.NavigationMessage = message;
            }

            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
