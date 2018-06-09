using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.Enum;
using showTracker.Model.View;
using Xamarin.Forms;

namespace showTracker.BusinessLayer.Services
{
    public class NavigationService : INavigationService
    {
        private Dictionary<ApplicationPageEnum, Type> _pageDictionary;

        public Dictionary<ApplicationPageEnum, Type> PageDictionary
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
            var page = ServiceLocator.Current.GetInstance(PageDictionary[pageType]);
            if (page is Page validPage)
            {
                if (validPage.BindingContext is BaseViewModel viewModel)
                {
                    viewModel.NavigationMessage = message;
                }                
                await Application.Current.MainPage.Navigation.PushAsync(validPage);
            }
        }

        public async Task NavigateBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
